namespace Td.Kylin.Searcher
{
    /// <summary>
    /// 操作方式
    /// </summary>
    public enum ActionMode
    {
        /// <summary>
        /// 初始化索引
        /// </summary>
        Init,
        /// <summary>
        /// 添加索引－根据ID
        /// </summary>
        InsertByID,
        /// <summary>
        /// 添加索引－根据对象
        /// </summary>
        InsertByEntity,
        /// <summary>
        /// 修改索引－根据ID
        /// </summary>
        ModifyByID,
        /// <summary>
        /// 修改索引－根据对象
        /// </summary>
        ModifyByEntity,
        /// <summary>
        /// 删除索引
        /// </summary>
        Delete
    }
}
