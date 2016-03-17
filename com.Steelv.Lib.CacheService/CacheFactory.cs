/* 
 * 
 * 文 件 名：Memcached.cs
 * 模块功能：Memcached类，Memcached缓存服务操作类
 * 建立时间：2015.12.30
 * 创 建 人：JiaoFeng(Tristan Jiao)
 * Email   : 1006200300@qq.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.Steelv.Lib.CacheService
{
    /// <summary>
    /// Cache构建工厂
    /// </summary>
    public class CacheFactory
    {
        #region 字段、属性

        private static Type _cacheType;

        /// <summary>
        /// 缓存类型
        /// </summary>
        public static Type CacheType
        {
            get
            {
                if (_cacheType == null)
                {
                    _cacheType = Type.GetType(CacheProperty.CACHE_NAMESPACE + "." + CacheProperty.ServerName, true, true);
                }
                return _cacheType;
            }
            set { _cacheType = value; }
        }

        #endregion

        /// <summary>
        /// 创建ICache实例
        /// </summary>
        /// <param name="cacheConnKey">缓存服务配置名称</param>
        /// <returns>Cache操作对象</returns>
        public static ICache CreateCache(string cacheConnKey)
        {
            string serverName = CacheProperty.ServerName;
            ICache _cache = null;
            try
            {
                Type type = CacheType;
                _cache = (ICache)Activator.CreateInstance(type, new object[] { cacheConnKey });
            }
            catch (Exception ex)
            {
                throw;
            }
            return _cache;
        }

    }
}
