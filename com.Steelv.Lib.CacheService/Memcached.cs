/* 
 * 
 * 文 件 名：Memcached.cs
 * 模块功能：Memcached类，Memcached缓存服务操作类
 * 建立时间：2011.10.11 * 建立时间：2015.12.30
 * 创 建 人：JiaoFeng(Tristan Jiao)
 * Email   : 1006200300@qq.com
 */
using System;
using System.Collections.Generic;
using System.Text;
using BeIT.MemCached;

namespace com.Steelv.Lib.CacheService
{
    public class Memcached : CacheBase, ICache
    {
        #region 字段

        private MemcachedClient _mcClient;
        private MemcachedClient McClient
        {
            get
            {
                return _mcClient;
            }
            set
            {
                _mcClient = value;
            }
        }

        #endregion

        #region 配置信息

        private string _instancename;
        /// <summary>
        /// 缓存实例名
        /// </summary>
        public string InstanceName
        {
            get { return _instancename; }
        }
        private string _serverlist;
        /// <summary>
        /// 缓存服务器列表
        /// </summary>
        public string ServerList
        {
            get { return _serverlist; }
        }
        private int _sendreceivetimeout;
        /// <summary>
        /// 超时
        /// </summary>
        public int SendReceiveTimeout
        {
            get { return _sendreceivetimeout; }
        }
        private uint _minPoolSize;
        /// <summary>
        /// 最小缓存池大小
        /// </summary>
        public uint MinPoolSize
        {
            get { return _minPoolSize; }
        }
        private uint _maxpoolsize;
        /// <summary>
        /// 最大缓存池大小
        /// </summary>    
        public uint Maxpoolsize
        {
            get { return _maxpoolsize; }
        }
        private bool _enabled = false;
        /// <summary>
        /// 是否启用缓存服务
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
        }
        private bool _hit = false;
        /// <summary>
        /// 是否命中
        /// </summary>
        public bool Hit
        {
            get { return _hit; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheConn">缓存连接配置Key</param>
        public Memcached()
        {
            GetSettings(CacheProperty.CACHE_GLOBAL);
            _mcClient = GetInstance();
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheConfigKey">缓存连接配置Key</param>
        public Memcached(string cacheConfigKey)
        {
            GetSettings(cacheConfigKey);
            _mcClient = GetInstance();
        }

        #endregion

        #region CacheBase

        /// <summary>
        /// 获取配置信息
        /// </summary>
        protected override void GetSettings(string cacheConnKey)
        {
            foreach (string strData in GetConnectionString(cacheConnKey).Split(';'))
            {
                string[] strArgs = strData.Split('=');
                if (strArgs.Length < 2) continue;
                string strKey = strArgs[0];
                string strValue = strArgs[1];

                switch (strKey.ToLower())
                {
                    case "instancename":
                        _instancename = strValue;
                        break;
                    case "serverlist":
                        _serverlist = strValue;
                        break;
                    case "sendreceivetimeout":
                        _sendreceivetimeout = int.Parse(strValue);
                        break;
                    case "minpoolsize":
                        _minPoolSize = uint.Parse(strValue);
                        break;
                    case "maxpoolsize":
                        _maxpoolsize = uint.Parse(strValue);
                        break;
                    case "enabled":
                        _enabled = bool.Parse(strValue);
                        break;
                }
            }
        }

        #endregion

        #region ICache

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="coverType">覆盖类型</param>
        public void Set(string key, object value, CoverType coverType = CoverType.set)
        {
            switch (coverType)
            {
                case CoverType.set:
                    _mcClient.Set(key, value);
                    break;
                case CoverType.add:
                    _mcClient.Add(key, value);
                    break;
                case CoverType.replace:
                    _mcClient.Replace(key, value);
                    break;
            }
        }

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expireTime">过期时间(分钟)，默认为0，指不过期</param>
        /// <param name="coverType">覆盖类型</param>
        public void Set(string key, object value, int expireTime, CoverType coverType = CoverType.set)
        {
            switch (coverType)
            {
                case CoverType.set:
                    _mcClient.Set(key, value, new TimeSpan(0, expireTime, 0));
                    break;
                case CoverType.add:
                    _mcClient.Add(key, value, new TimeSpan(0, expireTime, 0));
                    break;
                case CoverType.replace:
                    _mcClient.Replace(key, value, new TimeSpan(0, expireTime, 0));
                    break;
            }
        }

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeSpan">过期时间</param>
        /// <param name="coverType">覆盖类型</param>
        public void Set(string key, object value, TimeSpan timeSpan, CoverType coverType = CoverType.set)
        {
            switch (coverType)
            {
                case CoverType.set:
                    _mcClient.Set(key, value, timeSpan);
                    break;
                case CoverType.add:
                    _mcClient.Add(key, value, timeSpan);
                    break;
                case CoverType.replace:
                    _mcClient.Replace(key, value, timeSpan);
                    break;
            }
        }

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="dataTime">过期日期</param>
        /// <param name="coverType">覆盖类型</param>
        public void Set(string key, object value, DateTime dataTime, CoverType coverType = CoverType.set)
        {
            switch (coverType)
            {
                case CoverType.set:
                    _mcClient.Set(key, value, dataTime);
                    break;
                case CoverType.add:
                    _mcClient.Add(key, value, dataTime);
                    break;
                case CoverType.replace:
                    _mcClient.Replace(key, value, dataTime);
                    break;
            }
        }

        /// <summary>
        /// 设置计数器
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetCounter(string key, ulong value)
        {
            _mcClient.SetCounter(key, value);
        }

        /// <summary>
        /// 获取计数器
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public ulong? GetCounter(string key)
        {
            ulong? value = _mcClient.GetCounter(key);
            _hit = (value.HasValue);
            return value;
        }


        /// <summary>
        /// 缓存对象自增长
        /// </summary>
        /// <param name="key">键</param>
        public ulong? Increment(string key)
        {
            return Increment(key, 1);
        }


        /// <summary>
        /// 缓存对象自增长
        /// </summary>
        /// <param name="key">键</param>
        public ulong? Increment(string key, ulong value)
        {
            ulong? count = _mcClient.Increment(key, value);

            if (!count.HasValue)
            {
                count = value;
                this.SetCounter(key, value);
            }
            return count.Value;
        }

        /// <summary>
        /// 删除缓存数据
        /// </summary>
        /// <param name="key">键</param>
        public void Delete(string key)
        {
            _mcClient.Delete(key);
        }

        /// <summary>
        /// 删除缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="dateSpan">阻塞时间</param>
        public void Delete(string key, TimeSpan dateSpan)
        {
            _mcClient.Delete(key, dateSpan);
        }

        /// <summary>
        /// 删除缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="dataTime">阻塞时间</param>
        public void Delete(string key, DateTime dataTime)
        {
            _mcClient.Delete(key, dataTime);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>数据对象</returns>
        public object Get(string key)
        {
            object data = _enabled ? _mcClient.Get(key) : null;
            _hit = (null != data);
            return data;
        }

        /// <summary>
        /// 获取缓存对象数组
        /// </summary>
        /// <param name="keys">键数组</param>
        /// <returns>对象数组</returns>
        public object[] Gets(params string[] keys)
        {
            object[] data = _enabled ? _mcClient.Get(keys) : null;
            _hit = (null != data);
            return data;
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="key">键</param>
        /// <returns>数据对象</returns>
        public T Get<T>(string key)
        {
            object obj = Get(key);
            if (obj == null)
            {
                return default(T);
            }
            else
            {
                return (T)obj;
            }
        }


        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="keys">键数组</param>
        /// <returns>对象List</returns>
        public List<T> Gets<T>(params string[] keys)
        {
            object[] data = this.Gets(keys);
            List<T> list = null;
            if (data != null)
            {
                list = new List<T>();
                foreach (object obj in data)
                {
                    list.Add((T)obj);
                }
            }
            return list;
        }


        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="dateSpan">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<TResult>(Func<TResult> myfun, string key)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun();
                Set(key, returnObj);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="dateSpan">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<TResult>(Func<TResult> myfun, string key, TimeSpan dateSpan)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun();
                Set(key, returnObj, dateSpan);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="dataTime">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<TResult>(Func<TResult> myfun, string key, DateTime dataTime)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun();
                Set(key, returnObj, dataTime);
            }

            return returnObj;
        }


        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="dateSpan">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, TResult>(Func<T1, TResult> myfun, T1 param1, string key)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1);
                Set(key, returnObj);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="dateSpan">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, TResult>(Func<T1, TResult> myfun, T1 param1, string key, TimeSpan dateSpan)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1);
                Set(key, returnObj, dateSpan);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="dataTime">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, TResult>(Func<T1, TResult> myfun, T1 param1, string key, DateTime dataTime)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1);
                Set(key, returnObj, dataTime);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, T2, TResult>(Func<T1, T2, TResult> myfun, T1 param1, T2 param2, string key)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1, param2);
                Set(key, returnObj);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <param name="dateSpan">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, T2, TResult>(Func<T1, T2, TResult> myfun, T1 param1, T2 param2, string key, TimeSpan dateSpan)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1, param2);
                Set(key, returnObj, dateSpan);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <param name="dataTime">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, T2, TResult>(Func<T1, T2, TResult> myfun, T1 param1, T2 param2, string key, DateTime dataTime)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1, param2);
                Set(key, returnObj, dataTime);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <param name="param3">方法的参数3</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> myfun, T1 param1, T2 param2, T3 param3, string key)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1, param2, param3);
                Set(key, returnObj);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <param name="param3">方法的参数3</param>
        /// <param name="dateSpan">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> myfun, T1 param1, T2 param2, T3 param3, string key, TimeSpan dateSpan)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1, param2, param3);
                Set(key, returnObj, dateSpan);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <param name="param3">方法的参数3</param>
        /// <param name="dataTime">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> myfun, T1 param1, T2 param2, T3 param3, string key, DateTime dataTime)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1, param2, param3);
                Set(key, returnObj, dataTime);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <param name="param3">方法的参数3</param>
        /// <param name="param4">方法的参数4</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> myfun, T1 param1, T2 param2, T3 param3, T4 param4, string key)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1, param2, param3, param4);
                Set(key, returnObj);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <param name="param3">方法的参数3</param>
        /// <param name="param4">方法的参数4</param>
        /// <param name="dateSpan">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> myfun, T1 param1, T2 param2, T3 param3, T4 param4, string key, TimeSpan dateSpan)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1, param2, param3, param4);
                Set(key, returnObj, dateSpan);
            }

            return returnObj;
        }

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <param name="param3">方法的参数3</param>
        /// <param name="param4">方法的参数4</param>
        /// <param name="dataTime">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        public TResult Get<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> myfun, T1 param1, T2 param2, T3 param3, T4 param4, string key, DateTime dataTime)
        {
            TResult returnObj;
            returnObj = Get<TResult>(key);

            if (!_hit && myfun != null)
            {
                returnObj = myfun(param1, param2, param3, param4);
                Set(key, returnObj, dataTime);
            }

            return returnObj;
        }

        /// <summary>
        /// 缓存全部失效
        /// </summary>
        public void FlushAll()
        {
            _mcClient.FlushAll();
        }

        /// <summary>
        /// 缓存全部失效（多久后失效）
        /// </summary>
        /// <param name="dateSpan">失效时间</param>
        public void FlushAll(TimeSpan dateSpan)
        {
            _mcClient.FlushAll(dateSpan);
        }


        #endregion

        /// <summary>
        /// 获取缓存实例
        /// </summary>
        /// <returns>缓存实例</returns>
        private MemcachedClient GetInstance()
        {
            MemcachedClient cache = null;
            if (MemcachedClient.Exists(_instancename) == false)
            {
                MemcachedClient.Setup(_instancename, _serverlist.Split(','));
            }

            cache = MemcachedClient.GetInstance(_instancename);

            cache.SendReceiveTimeout = _sendreceivetimeout;
            cache.MinPoolSize = _minPoolSize;
            cache.MaxPoolSize = _maxpoolsize;
            return cache;
        }


    }
}
