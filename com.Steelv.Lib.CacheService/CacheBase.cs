/* 
 * 
 * 文 件 名：CacheBase.cs
 * 模块功能：CacheBase抽象类，获取配置数据和基础方法
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
    /// Cache基类，配置数据和基础方法
    /// </summary>
    public abstract class CacheBase
    {
        #region 字段

        private string _connectionStr;

        #endregion

        #region 方法

        /// <summary>
        /// 配置数据
        /// </summary>
        /// <param name="connKey"></param>
        protected string GetConnectionString(string connKey)
        {
            if (!string.IsNullOrEmpty(_connectionStr))
                return _connectionStr;
            else
            {
                var settings = System.Configuration.ConfigurationManager.AppSettings[connKey];
                if (string.IsNullOrEmpty(settings))
                {
                    throw new Exception("未添加Cache服务器配置节点");
                }
                _connectionStr = settings;
                return _connectionStr;
            }
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="cacheConnKey"></param>
        protected abstract void GetSettings(string cacheConnKey);

        #endregion

    }
}
