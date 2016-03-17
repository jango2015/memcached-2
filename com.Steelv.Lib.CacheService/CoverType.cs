using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.Steelv.Lib.CacheService
{
    /// <summary>
    /// 覆盖类型
    /// </summary>
    public enum CoverType
    {
        /// <summary>
        /// 无论何时都保存
        /// </summary>
        set,

        /// <summary>
        /// 仅当存储空间中不存在键相同的数据时才保存
        /// </summary>
        add,

        /// <summary>
        /// 仅当存储空间中存在键相同的数据时才保存
        /// </summary>
        replace
    }
}
