/* 
* 
* 文 件 名：ICache.cs
* 模块功能：ICache接口，缓存服务操作接口
* 建立时间：2015.12.30
* 创 建 人：JiaoFeng(Tristan Jiao)
* Email   : 1006200300@qq.com
* 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.Steelv.Lib.CacheService
{
    /// <summary>
    /// 缓存服务操作接口
    /// 
    ///     demo:
    ///     var _cache = CacheFactory.CreateCache("Cache.Test");
    ///     _cache.Set("key1", "aa44a", 1);
    ///     var result3 = _cache.Get("key1");
    /// 
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 是否命中
        /// </summary>
        bool Hit { get; }

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="coverType">覆盖类型</param>
        void Set(string key, object value, CoverType coverType = CoverType.set);

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expireTime">过期时间（分钟），默认为0，指不过期</param>
        /// <param name="coverType">覆盖类型</param>
        void Set(string key, object value, int expireTime, CoverType coverType = CoverType.set);

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeSpan">过期时间</param>
        /// <param name="coverType">覆盖类型</param>
        void Set(string key, object value, TimeSpan timeSpan, CoverType coverType = CoverType.set);

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="dataTime">过期日期</param>
        /// <param name="coverType">覆盖类型</param>
        void Set(string key, object value, DateTime dataTime, CoverType coverType = CoverType.set);

        /// <summary>
        /// 设置计数器
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void SetCounter(string key, ulong value);

        /// <summary>
        /// 获取计数器
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        ulong? GetCounter(string key);

        /// <summary>
        /// 缓存对象自增长
        /// </summary>
        /// <param name="key">键</param>
        ulong? Increment(string key);

        /// <summary>
        /// 缓存对象自增长
        /// </summary>
        /// <param name="key">键</param>
        ulong? Increment(string key, ulong value);

        /// <summary>
        /// 删除缓存数据
        /// </summary>
        /// <param name="key">键</param>
        void Delete(string key);

        /// <summary>
        /// 删除缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="dateSpan">阻塞时间</param>
        void Delete(string key, TimeSpan dateSpan);

        /// <summary>
        /// 删除缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="dataTime">阻塞时间</param>
        void Delete(string key, DateTime dataTime);

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>数据对象</returns>
        object Get(string key);

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="key">键</param>
        /// <returns>数据对象</returns>
        T Get<T>(string key);

        /// <summary>
        /// 获取缓存对象数组
        /// </summary>
        /// <param name="keys">键数组</param>
        /// <returns>对象数组</returns>
        object[] Gets(params string[] keys);

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="keys">键数组</param>
        /// <returns>对象List</returns>
        List<T> Gets<T>(params string[] keys);


        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <returns>返回TResult</returns>
        TResult Get<TResult>(Func<TResult> myfun, string key);

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="dateSpan">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        TResult Get<TResult>(Func<TResult> myfun, string key, TimeSpan dateSpan);

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="dataTime">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        TResult Get<TResult>(Func<TResult> myfun, string key, DateTime dataTime);

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <returns>返回TResult</returns>
        TResult Get<T1, TResult>(Func<T1, TResult> myfun, T1 param1, string key);

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="dateSpan">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        TResult Get<T1, TResult>(Func<T1, TResult> myfun, T1 param1, string key, TimeSpan dateSpan);

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="dataTime">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        TResult Get<T1, TResult>(Func<T1, TResult> myfun, T1 param1, string key, DateTime dataTime);

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <returns>返回TResult</returns>
        TResult Get<T1, T2, TResult>(Func<T1, T2, TResult> myfun, T1 param1, T2 param2, string key);

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <param name="dateSpan">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        TResult Get<T1, T2, TResult>(Func<T1, T2, TResult> myfun, T1 param1, T2 param2, string key, TimeSpan dateSpan);

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <param name="dataTime">缓存失效时间</param>
        /// <returns>返回TResult</returns>
        TResult Get<T1, T2, TResult>(Func<T1, T2, TResult> myfun, T1 param1, T2 param2, string key, DateTime dataTime);

        /// <summary>
        /// 根据key得到缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="myfun">缓存失效后，调用的方法名</param>
        /// <param name="param1">方法的参数1</param>
        /// <param name="param2">方法的参数2</param>
        /// <param name="param3">方法的参数3</param>
        /// <returns>返回TResult</returns>
        TResult Get<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> myfun, T1 param1, T2 param2, T3 param3, string key);

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
        TResult Get<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> myfun, T1 param1, T2 param2, T3 param3, string key, TimeSpan dateSpan);

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
        TResult Get<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> myfun, T1 param1, T2 param2, T3 param3, string key, DateTime dataTime);

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
        TResult Get<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> myfun, T1 param1, T2 param2, T3 param3, T4 param4, string key);

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
        TResult Get<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> myfun, T1 param1, T2 param2, T3 param3, T4 param4, string key, TimeSpan dateSpan);

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
        TResult Get<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> myfun, T1 param1, T2 param2, T3 param3, T4 param4, string key, DateTime dataTime);

        /// <summary>
        /// 缓存全部失效
        /// </summary>
        void FlushAll();

        /// <summary>
        /// 缓存全部失效（多久后失效）
        /// </summary>
        /// <param name="dateSpan">失效时间</param>
        void FlushAll(TimeSpan dateSpan);
        //void FlushAll(DateTime dataTime);
    }

}
        

        
        
        
        
        
        
        

        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        

        
        
        
        
        
        

        
        
        
        
        
        

        
        
        
        
        

        
        
        
        
        

        
        
        
        
        

        
        
        
        
        
        

        
        
        
        
        
        

        
        
        
        
        
        

        
        
        
        
        
        
        

        
        
        
        
        
        

        
        
        
        
        
        
        


        
        
        
        
        
        
        

        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        
        
        


        
        
        
        
        
        
        
        
        
        


        
        
        
        
        
        
        
        
        
        


        
        
        
        
        
        
        
        
        
        
        


        
        
        
        
        
        
        
        
        
        
        


        
        
        
        
        
        
        
        
        
        
        


        
        
        
        
        
        
        
        
        
        
        
        


        
        
        
        
        
        
        
        
        
        
        
        


        
        
        
        

        
        
        
        
        
        