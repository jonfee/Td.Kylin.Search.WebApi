using Lucene.Net.Index;
using System.Collections.Generic;
using System.Threading;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.Enums;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.WriterManager
{
    public abstract class BaseIndexManager<T> where T : BaseIndexModel
    {
        public BaseIndexManager()
        {
            indexQueue = new Queue<QueueModel>();
        }

        /// <summary>
        /// 索引数据操作队列
        /// </summary>
        Queue<QueueModel> indexQueue;

        /// <summary>
        /// 队列是否正在处理
        /// </summary>
        private volatile static bool queueInProcessing = false;

        /// <summary>
        /// 将新文件添加到队列结尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Insert(T item)
        {
            if (null != item)
            {
                QueueModel model = new QueueModel();
                model.ActionMode = ActionMode.Insert;
                model.Data = item;
                model.DataType = item.DataType;
                model.ID = item.ID;
                model.AreaID = item.AreaID;

                indexQueue.Enqueue(model);
            }
        }

        /// <summary>
        /// 将新文件添加到队列结尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public void Insert(IEnumerable<T> collection)
        {
            if (null != collection)
            {
                foreach (var item in collection)
                {
                    Insert(item);
                }
            }
        }

        /// <summary>
        /// 将更新文件添加到队列结尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Modify(T item)
        {
            if (null != item)
            {
                QueueModel model = new QueueModel();
                model.ActionMode = ActionMode.Modify;
                model.Data = item;
                model.DataType = item.DataType;
                model.ID = item.ID;
                model.AreaID = item.AreaID;

                indexQueue.Enqueue(model);
            }
        }

        /// <summary>
        /// 将更新文件添加到队列结尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public void Modify(IEnumerable<T> collection)
        {
            if (null != collection)
            {
                foreach (var item in collection)
                {
                    Modify(item);
                }
            }
        }

        /// <summary>
        /// 将删除文件添加到队列结尾
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="areaID"></param>
        /// <param name="id"></param>
        public void Delete(IndexDataType dataType, int areaID, long id)
        {
            if (id > 0)
            {
                QueueModel model = new QueueModel();
                model.ActionMode = ActionMode.Delete;
                model.Data = null;
                model.DataType = dataType;
                model.ID = id;
                model.AreaID = areaID;

                indexQueue.Enqueue(model);
            }
        }

        /// <summary>
        /// 将删除文件添加到队列结尾
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="areaID"></param>
        /// <param name="ids"></param>
        public void Delete(IndexDataType dataType, int areaID, IEnumerable<long> ids)
        {
            if (null != ids)
            {
                foreach (var id in ids)
                {
                    Delete(dataType, areaID, id);
                }
            }
        }

        /// <summary>
        /// 开启线程执行队列操作
        /// </summary>
        public void StartNewThread()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(QueueToIndex));
        }

        /// <summary>
        /// 将队列中的数据取出来插入索引库中
        /// </summary>
        /// <param name="para"></param>
        private void QueueToIndex(object para)
        {
            while (true)
            {
                if (indexQueue.Count > 0 && queueInProcessing == false)
                {
                    IndexDoWork();
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }

        /// <summary>
        /// 获取IndexWriter
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected abstract IndexWriter GetIndex(QueueModel state);

        /// <summary>
        /// 优化并提交索引库（IndexWriter）
        /// </summary>
        protected abstract void Commit();

        /// <summary>
        /// 释放资源（IndexWriter及FSDirectory）
        /// </summary>
        protected virtual void Dispose()
        {
            //释放IndexWriter
            //释放FSDirectory
        }

        /// <summary>
        /// 更新索引库操作
        /// </summary>
        private void IndexDoWork()
        {
            //处理中
            queueInProcessing = true;

            while (indexQueue.Count > 0)
            {
                QueueModel model = indexQueue.Dequeue();

                if (null == model) continue;

                var data = model.Data;

                if (model.ActionMode != ActionMode.Delete && data == null) continue;

                //当前IndexWriter
                var writer = GetIndex(model);

                if (null == writer) continue;

                switch (model.ActionMode)
                {
                    case ActionMode.Delete: 
                        DeleteIndex(writer, model.ID);
                        break;
                    case ActionMode.Insert:
                        AddIndex(writer, data);
                        break;
                    case ActionMode.Modify:
                        ModifyIndex(writer, data);
                        break;
                }
            }

            //优化并提交
            Commit();

            //释放资源
            Dispose();

            queueInProcessing = false;
        }

        /// <summary>
        /// 添加数据到索引文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void AddIndex(IndexWriter writer, BaseIndexModel item)
        {
            if (null == writer || null == item) return;

            var document = new DocumentFactory(item).Result;

            if (null != document)
            {
                writer.AddDocument(document);
            }
        }

        /// <summary>
        /// 删除数据到索引文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="id"></param>
        private void DeleteIndex(IndexWriter writer, long id)
        {
            if (null == writer) return;

            try
            {
                writer.DeleteDocuments(new Term("id", id.ToString()));
            }
            catch { }
        }

        /// <summary>
        /// 修改数据到索引文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void ModifyIndex(IndexWriter writer, BaseIndexModel item)
        {
            DeleteIndex(writer, item.ID);
            AddIndex(writer, item);
        }
    }
}
