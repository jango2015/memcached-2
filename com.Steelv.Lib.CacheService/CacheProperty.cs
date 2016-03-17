using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.Steelv.Lib.CacheService
{

    /// <summary>
    /// Cache属性
    /// </summary>
    public class CacheProperty
    {
        #region 常量

        /// <summary>
        /// 缓存服务器名称
        /// </summary>
        public const string CACHE_NAME = "CacheName";
        /// <summary>
        /// 全局缓存配置名称
        /// </summary>
        public const string CACHE_GLOBAL = "Cache.Global";

        /// <summary>
        /// 命名空间
        /// </summary>
        public const string CACHE_NAMESPACE = "com.Steelv.Lib.CacheService";

        public const string MEMCACHED = "Memcached";

        #endregion

        #region 字段

        private static string _serverName;

        #endregion

        /// <summary>
        /// 服务器类型名称
        /// </summary>
        public static string ServerName
        {
            get
            {
                if (string.IsNullOrEmpty(_serverName))
                {//InstanceName=Test;
                    var settings = System.Configuration.ConfigurationManager.AppSettings[CACHE_NAME];
                    if (string.IsNullOrEmpty(settings))
                    {
                        throw new Exception("未添加CacheName服务器配置节点");
                    }
                    _serverName = settings;
                }
                return _serverName;
            }
        }
    }
}
