define({ "api": [
  {
    "version": "1.0.0",
    "description": "<p>向索引库中新增商家招聘。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/job/add"
      }
    ],
    "type": "Post",
    "url": "/v1/job/add",
    "title": "向索引库中新增商家招聘",
    "name": "Add",
    "group": "Job",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "jobID",
            "description": "<p>招聘ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/JobController.cs",
    "groupTitle": "Job"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中删除商家招聘。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/job/delete"
      }
    ],
    "type": "Post",
    "url": "/v1/job/delete",
    "title": "向索引库中删除商家招聘",
    "name": "Delete",
    "group": "Job",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "areaID",
            "description": "<p>招聘所属区域ID（为0或为null时表示由系统检测并处理）</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "jobID",
            "description": "<p>招聘ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/JobController.cs",
    "groupTitle": "Job"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中新增商家招聘。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/job/insert"
      }
    ],
    "type": "Post",
    "url": "/v1/job/insert",
    "title": "向索引库中新增商家招聘",
    "name": "Insert",
    "group": "Job",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "examples": [
        {
          "title": "item 招聘信息",
          "content": "{\n    \"MerchantID\": long                  商家ID\n    \"MerchantName\": string              商家名称\n    \"MerchantCertificateStatus\": int    商家认证类型（以2的N次方和表示）\n    \"CategoryID\": long                  岗位类型ID\n    \"Count\": int                        招聘人数\n    \"MinMonthly\": int                   最低提供月薪\n    \"MaxMonthly\": int                   最高提供月薪\n    \"Sex\": int                          性别（1 男，2 女）\n    \"MinAge\": int                       最小年龄限制\n    \"MaxAge\": int                       最大年龄限制\n    \"MinEducation\": int                 最低学历要求（枚举）\n    \"MinJobYearsType\": int              最低工作年限\n    \"JobType\": int                      工作性质（1 全职，2 兼职）\n    \"Welfares\": int                     福利（枚举，以2的N次方和表示）\n    \"WordAddress\": string               工作地点\n    \"Latitude\": double                  工作地纬度\n    \"Longitude\": double                 工作地经度\n    \"ID\": long                          招聘信息ID\n    \"AreaID\": int                       招聘所属区域（一般为具体区县ID）\n    \"AreaLayer\": string                 招聘所属区域路径（如：440000,440300,440301 表示：广东省深圳市罗湖区）\n    \"Name\": string                      岗位名称\n    \"CreateTime\": datetime              发布时间\n}",
          "type": "object"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/JobController.cs",
    "groupTitle": "Job"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中更新商家招聘。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/job/modify"
      }
    ],
    "type": "Post",
    "url": "/v1/job/modify",
    "title": "向索引库中更新商家招聘",
    "name": "Modify",
    "group": "Job",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "jobID",
            "description": "<p>招聘ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/JobController.cs",
    "groupTitle": "Job"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中更新商家招聘。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/job/update"
      }
    ],
    "type": "Post",
    "url": "/v1/job/update",
    "title": "向索引库中更新商家招聘",
    "name": "Update",
    "group": "Job",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "examples": [
        {
          "title": "item 招聘信息",
          "content": "{\n    \"MerchantID\": long                  商家ID\n    \"MerchantName\": string              商家名称\n    \"MerchantCertificateStatus\": int    商家认证类型（以2的N次方和表示）\n    \"CategoryID\": long                  岗位类型ID\n    \"Count\": int                        招聘人数\n    \"MinMonthly\": int                   最低提供月薪\n    \"MaxMonthly\": int                   最高提供月薪\n    \"Sex\": int                          性别（1 男，2 女）\n    \"MinAge\": int                       最小年龄限制\n    \"MaxAge\": int                       最大年龄限制\n    \"MinEducation\": int                 最低学历要求（枚举）\n    \"MinJobYearsType\": int              最低工作年限\n    \"JobType\": int                      工作性质（1 全职，2 兼职）\n    \"Welfares\": int                     福利（枚举，以2的N次方和表示）\n    \"WordAddress\": string               工作地点\n    \"Latitude\": double                  工作地纬度\n    \"Longitude\": double                 工作地经度\n    \"ID\": long                          招聘信息ID\n    \"AreaID\": int                       招聘所属区域（一般为具体区县ID）\n    \"AreaLayer\": string                 招聘所属区域路径（如：440000,440300,440301 表示：广东省深圳市罗湖区）\n    \"Name\": string                      岗位名称\n    \"CreateTime\": datetime              发布时间\n}",
          "type": "object"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/JobController.cs",
    "groupTitle": "Job"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中新增精品汇（B2C）商品。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/mallproduct/add"
      }
    ],
    "type": "Post",
    "url": "/v1/mallproduct/add",
    "title": "向索引库中新增精品汇（B2C）商品",
    "name": "Add",
    "group": "MallProduct",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "productID",
            "description": "<p>商品ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MallProductController.cs",
    "groupTitle": "MallProduct"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中删除精品汇（B2C）商品。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/mallproduct/delete"
      }
    ],
    "type": "Post",
    "url": "/v1/mallproduct/delete",
    "title": "向索引库中删除精品汇（B2C）商品",
    "name": "Delete",
    "group": "MallProduct",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "areaID",
            "description": "<p>商品所属区域ID（为0或为null时表示由系统检测并处理）</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "productID",
            "description": "<p>商品ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MallProductController.cs",
    "groupTitle": "MallProduct"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中更新精品汇（B2C）商品。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/mallproduct/modify"
      }
    ],
    "type": "Post",
    "url": "/v1/mallproduct/modify",
    "title": "向索引库中更新精品汇（B2C）商品",
    "name": "Modify",
    "group": "MallProduct",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "productID",
            "description": "<p>商品ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MallProductController.cs",
    "groupTitle": "MallProduct"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中更新精品汇（B2C）商品。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/mallproduct/update"
      }
    ],
    "type": "Post",
    "url": "/v1/mallproduct/update",
    "title": "向索引库中更新精品汇（B2C）商品",
    "name": "Update",
    "group": "MallProduct",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "examples": [
        {
          "title": "item 精品汇（B2C）商品信息",
          "content": "{\n    \"ProductID\": long           商品ID\n     \"Specs\": string            规格名称\n     \"SKU\": string              规格SKU码\n     \"CategoryID\": long,        分类ID\n     \"CategoryName\": string,    分类名称\n     \"TagIDs\": string           商品标签ID集，如：\"6278435570435817519,6278435570435817518\"\n     \"TagNames\": string         商品标签名称集，如：\"智能手机,国产\"\n     \"MarketPrice\": decimal,    市场价\n     \"SalePrice\": decimal,      销售价\n     \"ID\": long,                规格SKUID          \n     \"AreaID\": int              所属区域ID\n     \"AreaLayer\": string        所属区域路径（如：440000,440300 表示：广东省深圳市）\n     \"Name\": string             商品名称\n     \"Pic\": string              商品图片（仅一张，不含文件服务器域名，如：mall/product/.../121.jpg）\n     \"CreateTime\": datetime     发布时间\n }",
          "type": "object"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MallProductController.cs",
    "groupTitle": "MallProduct"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中新增商家。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/merchant/add"
      }
    ],
    "type": "Post",
    "url": "/v1/merchant/add",
    "title": "向索引库中新增商家",
    "name": "Add",
    "group": "Merchant",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "merchantID",
            "description": "<p>商家ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MerchantController.cs",
    "groupTitle": "Merchant"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中删除商家。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/merchant/delete"
      }
    ],
    "type": "Post",
    "url": "/v1/merchant/delete",
    "title": "向索引库中删除商家",
    "name": "Delete",
    "group": "Merchant",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "areaID",
            "description": "<p>商家所属区域ID（为0或为null时表示由系统检测并处理）</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "merchantID",
            "description": "<p>商家ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MerchantController.cs",
    "groupTitle": "Merchant"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中新增商家。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/merchant/insert"
      }
    ],
    "type": "Post",
    "url": "/v1/merchant/insert",
    "title": "向索引库中新增商家",
    "name": "Insert",
    "group": "Merchant",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "examples": [
        {
          "title": "item 商家信息",
          "content": "{\n    \"Mobile\": string            手机号\n    \"IndustryID\": long          行业ID\n    \"IndustryName\": string      行业名称\n    \"LocationPlace\": string     所在位置\n    \"Street\": string            所在街道\n    \"Latitude\": double          纬度\n    \"Longitude\": double         经度\n    \"LinkMan\": string           联系人\n    \"Phone\": string             联系电话\n    \"CertificateStatus\": int    认证类型（以2的N次方和表示）\n    \"BusinessBeginTime\": string 开始营业时间（如：09:00）\n    \"BusinessEndTime\": string   结束营业时间（如：22:00）\n    \"ID\": long                  商家ID,\n    \"AreaID\": int               商家所属区域（一般为具体区县ID）\n    \"AreaLayer\": string         商家所属区域路径（如：440000，440300，440301 表示：广东省深圳市罗湖区）\n    \"Name\": string              商家名称\n    \"Pic\": string               商家图片路径（仅一张，不含文件服务器域名，如：enterprise/.../1231.jpg）\n    \"CreateTime\": datetime      创建时间\n}",
          "type": "object"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MerchantController.cs",
    "groupTitle": "Merchant"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中更新商家。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/merchant/modify"
      }
    ],
    "type": "Post",
    "url": "/v1/merchant/modify",
    "title": "向索引库中更新商家",
    "name": "Modify",
    "group": "Merchant",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "productID",
            "description": "<p>商品ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MerchantController.cs",
    "groupTitle": "Merchant"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中新增附近购（商家）商品。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/merchantproduct/add"
      }
    ],
    "type": "Post",
    "url": "/v1/merchantproduct/add",
    "title": "向索引库中新增附近购（商家）商品",
    "name": "Add",
    "group": "MerchantProduct",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "productID",
            "description": "<p>商品ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MerchantProductController.cs",
    "groupTitle": "MerchantProduct"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中删除附近购（商家）商品。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/merchantproduct/delete"
      }
    ],
    "type": "Post",
    "url": "/v1/merchantproduct/delete",
    "title": "向索引库中删除附近购（商家）商品",
    "name": "Delete",
    "group": "MerchantProduct",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "areaID",
            "description": "<p>商品所属区域ID（为0或为null时表示由系统检测并处理）</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "productID",
            "description": "<p>商品ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MerchantProductController.cs",
    "groupTitle": "MerchantProduct"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中新增附近购（商家）商品。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/merchantproduct/insert"
      }
    ],
    "type": "Post",
    "url": "/v1/merchantproduct/insert",
    "title": "向索引库中新增附近购（商家）商品",
    "name": "Insert",
    "group": "MerchantProduct",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "examples": [
        {
          "title": "item 商家商品信息",
          "content": "{\n   \"MerchantID\": long              所属商家ID\n   \"MerchantName\": string          所属商家名称\n   \"SystemCategoryID\": lont        商品所属系统分类ID\n   \"SystemCategoryName\": string    商品所属系统分类名称\n   \"OriginalPrice\": decimal        商品原价\n   \"SalePrice\": decimal            销售价\n   \"Specification\": string         规格\n   \"Latitude\": double              商家所在位置纬度\n   \"Longitude\": double             商家所在位置经度\n   \"ID\": long                      商品ID\n   \"AreaID\": int                   所属商家所在区域ID\n   \"AreaLayer\": string             所属商家所在区域路径（如：440000,440300 表示：广东省深圳市）\n   \"Name\": string                  商品名称\n   \"Pic\": string                   商品图片（仅一张，不含文件服务器域名，如：\"micromall/goods/16/05/05/113349i8bwct8ayd.jpg\"）\n   \"CreateTime\": datetime          发布时间\n}",
          "type": "object"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MerchantProductController.cs",
    "groupTitle": "MerchantProduct"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中更新附近购（商家）商品。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/merchantproduct/modify"
      }
    ],
    "type": "Post",
    "url": "/v1/merchantproduct/modify",
    "title": "向索引库中更新附近购（商家）商品",
    "name": "Modify",
    "group": "MerchantProduct",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "productID",
            "description": "<p>商品ID</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MerchantProductController.cs",
    "groupTitle": "MerchantProduct"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中更新附近购（商家）商品。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/merchantproduct/update"
      }
    ],
    "type": "Post",
    "url": "/v1/merchantproduct/update",
    "title": "向索引库中更新附近购（商家）商品",
    "name": "Update",
    "group": "MerchantProduct",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "examples": [
        {
          "title": "item 商家商品信息",
          "content": "{\n   \"MerchantID\": long              所属商家ID\n   \"MerchantName\": string          所属商家名称\n   \"SystemCategoryID\": lont        商品所属系统分类ID\n   \"SystemCategoryName\": string    商品所属系统分类名称\n   \"OriginalPrice\": decimal        商品原价\n   \"SalePrice\": decimal            销售价\n   \"Specification\": string         规格\n   \"Latitude\": double              商家所在位置纬度\n   \"Longitude\": double             商家所在位置经度\n   \"ID\": long                      商品ID\n   \"AreaID\": int                   所属商家所在区域ID\n   \"AreaLayer\": string             所属商家所在区域路径（如：440000,440300 表示：广东省深圳市）\n   \"Name\": string                  商品名称\n   \"Pic\": string                   商品图片（仅一张，不含文件服务器域名，如：\"micromall/goods/16/05/05/113349i8bwct8ayd.jpg\"）\n   \"CreateTime\": datetime          发布时间\n}",
          "type": "object"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MerchantProductController.cs",
    "groupTitle": "MerchantProduct"
  },
  {
    "version": "1.0.0",
    "description": "<p>向索引库中更新商家。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/merchant/update"
      }
    ],
    "type": "Post",
    "url": "/v1/merchant/update",
    "title": "向索引库中更新商家",
    "name": "Update",
    "group": "Merchant",
    "permission": [
      {
        "name": "Admin|Editor"
      }
    ],
    "parameter": {
      "examples": [
        {
          "title": "item 商家信息",
          "content": "{\n    \"Mobile\": string            手机号\n    \"IndustryID\": long          行业ID\n    \"IndustryName\": string      行业名称\n    \"LocationPlace\": string     所在位置\n    \"Street\": string            所在街道\n    \"Latitude\": double          纬度\n    \"Longitude\": double         经度\n    \"LinkMan\": string           联系人\n    \"Phone\": string             联系电话\n    \"CertificateStatus\": int    认证类型（以2的N次方和表示）\n    \"BusinessBeginTime\": string 开始营业时间（如：09:00）\n    \"BusinessEndTime\": string   结束营业时间（如：22:00）\n    \"ID\": long                  商家ID,\n    \"AreaID\": int               商家所属区域（一般为具体区县ID）\n    \"AreaLayer\": string         商家所属区域路径（如：440000，440300，440301 表示：广东省深圳市罗湖区）\n    \"Name\": string              商家名称\n    \"Pic\": string               商家图片路径（仅一张，不含文件服务器域名，如：enterprise/.../1231.jpg）\n    \"CreateTime\": datetime      创建时间\n}",
          "type": "object"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "正常输出: ",
          "content": "{}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/MerchantController.cs",
    "groupTitle": "Merchant"
  },
  {
    "version": "1.0.0",
    "description": "<p>搜索商家数据。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/search/job"
      }
    ],
    "type": "get",
    "url": "/v1/search/job",
    "title": "搜索商家数据",
    "name": "JobSearch",
    "group": "Search",
    "permission": [
      {
        "name": "Use"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "areaID",
            "description": "<p>区域ID（可为null，为null时表示不限制区域）</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>string</p> ",
            "optional": false,
            "field": "keyword",
            "description": "<p>关键词</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "pageIndex",
            "description": "<p>当前搜索页码</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "pageSize",
            "description": "<p>每页显示数量</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正确输出：",
          "content": "Content:{\n   Count:int   总记录数\n   Data:[\n       {\n           \"MerchantID\": long                  商家ID\n           \"MerchantName\": string              商家名称\n           \"MerchantCertificateStatus\": int    商家认证类型（以2的N次方和表示）\n           \"CategoryID\": long                  岗位类型ID\n           \"Count\": int                        招聘人数\n           \"MinMonthly\": int                   最低提供月薪\n           \"MaxMonthly\": int                   最高提供月薪\n           \"Sex\": int                          性别（1 男，2 女）\n           \"MinAge\": int                       最小年龄限制\n           \"MaxAge\": int                       最大年龄限制\n           \"MinEducation\": int                 最低学历要求（枚举）\n           \"MinJobYearsType\": int              最低工作年限\n           \"JobType\": int                      工作性质（1 全职，2 兼职）\n           \"Welfares\": int                     福利（枚举，以2的N次方和表示）\n           \"WordAddress\": string               工作地点\n           \"Latitude\": double                  工作地纬度\n           \"Longitude\": double                 工作地经度\n           \"ID\": long                          招聘信息ID\n           \"DataType\": 4                       数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）\n           \"AreaID\": int                       招聘所属区域（一般为具体区县ID）\n           \"AreaLayer\": string                 招聘所属区域路径（如：440000,440300,440301 表示：广东省深圳市罗湖区）\n           \"Name\": string                      岗位名称\n           \"Pic\": string                       （空，可忽略）\n           \"CreateTime\": datetime              发布时间\n           \"UpdateTime\": datetime              最后更新时间\n           \"Desc\": string                      描述（无实际意义，可忽略）\n       }\n   ]\n}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/SearchController.cs",
    "groupTitle": "Search"
  },
  {
    "version": "1.0.0",
    "description": "<p>搜索精品汇商品数据。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/search/mallproduct"
      }
    ],
    "type": "get",
    "url": "/v1/search/mallproduct",
    "title": "搜索精品汇商品数据",
    "name": "MallProductSearch",
    "group": "Search",
    "permission": [
      {
        "name": "Use"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "areaID",
            "description": "<p>区域ID（可为null，为null时表示不限制区域）</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>string</p> ",
            "optional": false,
            "field": "keyword",
            "description": "<p>关键词</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "pageIndex",
            "description": "<p>当前搜索页码</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "pageSize",
            "description": "<p>每页显示数量</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正确输出：",
          "content": "Content:{\n   Count:int   总记录数\n   Data:[\n       {\n           \"ProductID\": long           商品ID\n            \"Specs\": string            规格名称\n            \"SKU\": string              规格SKU码\n            \"CategoryID\": long,        分类ID\n            \"CategoryName\": string,    分类名称\n            \"TagIDs\": string           商品标签ID集，如：\"6278435570435817519,6278435570435817518\"\n            \"TagNames\": string         商品标签名称集，如：\"智能手机,国产\"\n            \"MarketPrice\": decimal,    市场价\n            \"SalePrice\": decimal,      销售价\n            \"ID\": long,                规格SKUID\n            \"DataType\": 1              数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）             \n            \"AreaID\": int              所属区域ID\n            \"AreaLayer\": string        所属区域路径（如：440000,440300 表示：广东省深圳市）\n            \"Name\": string             商品名称\n            \"Pic\": string              商品图片（仅一张，不含文件服务器域名，如：mall/product/.../121.jpg）\n            \"CreateTime\": datetime     发布时间\n            \"UpdateTime\": datetime     最后更新时间\n            \"Desc\": string             描述（无实际意义，可忽略）\n        }\n   ]\n}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/SearchController.cs",
    "groupTitle": "Search"
  },
  {
    "version": "1.0.0",
    "description": "<p>搜索附近购（商家）商品数据。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/search/merchantproduct"
      }
    ],
    "type": "get",
    "url": "/v1/search/merchantproduct",
    "title": "搜索附近购（商家）商品数据",
    "name": "MerchantProductSearch",
    "group": "Search",
    "permission": [
      {
        "name": "Use"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "areaID",
            "description": "<p>区域ID（可为null，为null时表示不限制区域）</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>string</p> ",
            "optional": false,
            "field": "keyword",
            "description": "<p>关键词</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "pageIndex",
            "description": "<p>当前搜索页码</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "pageSize",
            "description": "<p>每页显示数量</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正确输出：",
          "content": "Content:{\n   Count:int   总记录数\n   Data:[\n       {\n          \"MerchantID\": long              所属商家ID\n          \"MerchantName\": string          所属商家名称\n          \"SystemCategoryID\": lont        商品所属系统分类ID\n          \"SystemCategoryName\": string    商品所属系统分类名称\n          \"OriginalPrice\": decimal        商品原价\n          \"SalePrice\": decimal            销售价\n          \"Specification\": string         规格\n          \"Latitude\": double              商家所在位置纬度\n          \"Longitude\": double             商家所在位置经度\n          \"ID\": long                      商品ID\n          \"DataType\": 2                   数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息） \n          \"AreaID\": int                   所属商家所在区域ID\n          \"AreaLayer\": string             所属商家所在区域路径（如：440000,440300 表示：广东省深圳市）\n          \"Name\": string                  商品名称\n          \"Pic\": string                   商品图片（仅一张，不含文件服务器域名，如：\"micromall/goods/16/05/05/113349i8bwct8ayd.jpg\"）\n          \"CreateTime\": datetime          发布时间\n          \"UpdateTime\": datetime          最后更新时间\n          \"Desc\": string                  描述（无实际意义，可忽略）\n       }\n   ]\n}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/SearchController.cs",
    "groupTitle": "Search"
  },
  {
    "version": "1.0.0",
    "description": "<p>搜索商家数据。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/search/merchant"
      }
    ],
    "type": "get",
    "url": "/v1/search/merchant",
    "title": "搜索商家数据",
    "name": "MerchantSearch",
    "group": "Search",
    "permission": [
      {
        "name": "Use"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "areaID",
            "description": "<p>区域ID（可为null，为null时表示不限制区域）</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>string</p> ",
            "optional": false,
            "field": "keyword",
            "description": "<p>关键词</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "pageIndex",
            "description": "<p>当前搜索页码</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "pageSize",
            "description": "<p>每页显示数量</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正确输出：",
          "content": "Content:{\n   Count:int   总记录数\n   Data:[\n       {\n           \"Mobile\": string            手机号\n           \"IndustryID\": long          行业ID\n           \"IndustryName\": string      行业名称\n           \"LocationPlace\": string     所在位置\n           \"Street\": string            所在街道\n           \"Latitude\": double          纬度\n           \"Longitude\": double         经度\n           \"LinkMan\": string           联系人\n           \"Phone\": string             联系电话\n           \"CertificateStatus\": int    认证类型（以2的N次方和表示）\n           \"BusinessBeginTime\": string 开始营业时间（如：09:00）\n           \"BusinessEndTime\": string   结束营业时间（如：22:00）\n           \"ID\": long                  商家ID,\n           \"DataType\": 3               数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）\n           \"AreaID\": int               商家所属区域（一般为具体区县ID）\n           \"AreaLayer\": string         商家所属区域路径（如：440000，440300，440301 表示：广东省深圳市罗湖区）\n           \"Name\": string              商家名称\n           \"Pic\": string               商家图片路径（仅一张，不含文件服务器域名，如：enterprise/.../1231.jpg）\n           \"CreateTime\": datetime      创建时间      \n           \"UpdateTime\": datetime      最后更新时间\n           \"Desc\": string              描述（无实际意义，可忽略）\n       }\n   ]\n}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/SearchController.cs",
    "groupTitle": "Search"
  },
  {
    "version": "1.0.0",
    "description": "<p>搜索区域数据。</p> ",
    "sampleRequest": [
      {
        "url": "/v1/search"
      }
    ],
    "type": "get",
    "url": "/v1/search",
    "title": "搜索区域数据",
    "name": "search",
    "group": "Search",
    "permission": [
      {
        "name": "Use"
      }
    ],
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "<p>long</p> ",
            "optional": false,
            "field": "areaID",
            "description": "<p>区域ID</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>string</p> ",
            "optional": false,
            "field": "keyword",
            "description": "<p>关键词</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "pageIndex",
            "description": "<p>当前搜索页码</p> "
          },
          {
            "group": "Parameter",
            "type": "<p>int</p> ",
            "optional": false,
            "field": "pageSize",
            "description": "<p>每页显示数量</p> "
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "正确输出：",
          "content": "Content:{\n   Count:int   总记录数\n   Data:[ //对象为以下4种对象类型（混合）\n       //商家\n       {\n           \"Mobile\": string            手机号\n           \"IndustryID\": long          行业ID\n           \"IndustryName\": string      行业名称\n           \"LocationPlace\": string     所在位置\n           \"Street\": string            所在街道\n           \"Latitude\": double          纬度\n           \"Longitude\": double         经度\n           \"LinkMan\": string           联系人\n           \"Phone\": string             联系电话\n           \"CertificateStatus\": int    认证类型（以2的N次方和表示）\n           \"BusinessBeginTime\": string 开始营业时间（如：09:00）\n           \"BusinessEndTime\": string   结束营业时间（如：22:00）\n           \"ID\": long                  商家ID,\n           \"DataType\": 3               数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）\n           \"AreaID\": int               商家所属区域（一般为具体区县ID）\n           \"AreaLayer\": string         商家所属区域路径（如：440000，440300，440301 表示：广东省深圳市罗湖区）\n           \"Name\": string              商家名称\n           \"Pic\": string               商家图片路径（仅一张，不含文件服务器域名，如：enterprise/.../1231.jpg）\n           \"CreateTime\": datetime      创建时间      \n           \"UpdateTime\": datetime      最后更新时间\n           \"Desc\": string              描述（无实际意义，可忽略）\n       },\n       //招聘\n       {\n           \"MerchantID\": long                  商家ID\n           \"MerchantName\": string              商家名称\n           \"MerchantCertificateStatus\": int    商家认证类型（以2的N次方和表示）\n           \"CategoryID\": long                  岗位类型ID\n           \"Count\": int                        招聘人数\n           \"MinMonthly\": int                   最低提供月薪\n           \"MaxMonthly\": int                   最高提供月薪\n           \"Sex\": int                          性别（1 男，2 女）\n           \"MinAge\": int                       最小年龄限制\n           \"MaxAge\": int                       最大年龄限制\n           \"MinEducation\": int                 最低学历要求（枚举）\n           \"MinJobYearsType\": int              最低工作年限\n           \"JobType\": int                      工作性质（1 全职，2 兼职）\n           \"Welfares\": int                     福利（枚举，以2的N次方和表示）\n           \"WordAddress\": string               工作地点\n           \"Latitude\": double                  工作地纬度\n           \"Longitude\": double                 工作地经度\n           \"ID\": long                          招聘信息ID\n           \"DataType\": 4                       数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）\n           \"AreaID\": int                       招聘所属区域（一般为具体区县ID）\n           \"AreaLayer\": string                 招聘所属区域路径（如：440000,440300,440301 表示：广东省深圳市罗湖区）\n           \"Name\": string                      岗位名称\n           \"Pic\": string                       （空，可忽略）\n           \"CreateTime\": datetime              发布时间\n           \"UpdateTime\": datetime              最后更新时间\n           \"Desc\": string                      描述（无实际意义，可忽略）\n       },\n       //精品汇（B2C）商品\n       {\n           \"ProductID\": long           商品ID\n            \"Specs\": string            规格名称\n            \"SKU\": string              规格SKU码\n            \"CategoryID\": long,        分类ID\n            \"CategoryName\": string,    分类名称\n            \"TagIDs\": string           商品标签ID集，如：\"6278435570435817519,6278435570435817518\"\n            \"TagNames\": string         商品标签名称集，如：\"智能手机,国产\"\n            \"MarketPrice\": decimal,    市场价\n            \"SalePrice\": decimal,      销售价\n            \"ID\": long,                规格SKUID\n            \"DataType\": 1              数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）             \n            \"AreaID\": int              所属区域ID\n            \"AreaLayer\": string        所属区域路径（如：440000,440300 表示：广东省深圳市）\n            \"Name\": string             商品名称\n            \"Pic\": string              商品图片（仅一张，不含文件服务器域名，如：mall/product/.../121.jpg）\n            \"CreateTime\": datetime     发布时间\n            \"UpdateTime\": datetime     最后更新时间\n            \"Desc\": string             描述（无实际意义，可忽略）\n        },\n       //附近购（商家）商家\n       {\n          \"MerchantID\": long              所属商家ID\n          \"MerchantName\": string          所属商家名称\n          \"SystemCategoryID\": lont        商品所属系统分类ID\n          \"SystemCategoryName\": string    商品所属系统分类名称\n          \"OriginalPrice\": decimal        商品原价\n          \"SalePrice\": decimal            销售价\n          \"Specification\": string         规格\n          \"Latitude\": double              商家所在位置纬度\n          \"Longitude\": double             商家所在位置经度\n          \"ID\": long                      商品ID\n          \"DataType\": 2                   数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息） \n          \"AreaID\": int                   所属商家所在区域ID\n          \"AreaLayer\": string             所属商家所在区域路径（如：440000,440300 表示：广东省深圳市）\n          \"Name\": string                  商品名称\n          \"Pic\": string                   商品图片（仅一张，不含文件服务器域名，如：\"micromall/goods/16/05/05/113349i8bwct8ayd.jpg\"）\n          \"CreateTime\": datetime          发布时间\n          \"UpdateTime\": datetime          最后更新时间\n          \"Desc\": string                  描述（无实际意义，可忽略）\n       }\n   ]\n}",
          "type": "json"
        }
      ]
    },
    "filename": "D:/20151120Git/Td.Kylin.Search.WebApi/src/Td.Kylin.Search.WebApi/Controllers/SearchController.cs",
    "groupTitle": "Search"
  }
] });