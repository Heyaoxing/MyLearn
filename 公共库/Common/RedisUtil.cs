using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Common
{
    public class RedisUtil
    {

        //static NewtonsoftSerializer serializer = new NewtonsoftSerializer();
        //static StackExchangeRedisCacheClient cacheClient = new StackExchangeRedisCacheClient(serializer);
        //private static IDatabase db = cacheClient.Database;

        private static string connstr ="127.0.0.1:6379,allowadmin=true";
        private static ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(connstr);
        private static IDatabase db = conn.GetDatabase(1);


        /// <summary>
        /// 获取系统的redis key前缀
        /// </summary>
        /// <param name="resourceid">资源Id</param>
        /// <returns></returns>
        public static string GetMyKey(string resourceid = "")
        {

            string Key = "report_";
            if (!string.IsNullOrWhiteSpace(resourceid))
            {
                Key = string.Format("report_res_{0}", resourceid);
            }
            return Key;
        }

        #region String 可以设置过期时间 同步

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public static bool SetStringKey(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {

            return db.StringSet(key, value, expiry);
        }

        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="arr">key</param>
        /// <returns></returns>
        public static bool SetStringKey(KeyValuePair<RedisKey, RedisValue>[] arr)
        {
            return db.StringSet(arr);
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool SetStringKey<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = JsonConvert.SerializeObject(obj);
            return db.StringSet(key, json, expiry);
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>

        public static RedisValue GetStringKey(string key)
        {
            return db.StringGet(key);
        }


        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKey">Redis Key集合</param>
        /// <returns></returns>
        public static RedisValue[] GetStringKey(List<RedisKey> listKey)
        {
            return db.StringGet(listKey.ToArray());
        }

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetStringKey<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(db.StringGet(key));
        }
        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public double StringIncrement(string key, double val = 1)
        {
            return db.StringIncrement(key, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public double StringDecrement(string key, double val = 1)
        {
            return db.StringDecrement(key, val);
        }

        #endregion

        #region String 可以设置过期时间 异步

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public static async Task<bool> SetStringKeyAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {

            return await db.StringSetAsync(key, value, expiry);
        }

        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="arr">key</param>
        /// <returns></returns>
        public static async Task<bool> SetStringKeyAsync(KeyValuePair<RedisKey, RedisValue>[] arr)
        {
            return await db.StringSetAsync(arr);
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static async Task<bool> SetStringKeyAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = JsonConvert.SerializeObject(obj);
            return await db.StringSetAsync(key, json, expiry);
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>

        public static async Task<RedisValue> GetStringKeyAsync(string key)
        {
            return await db.StringGetAsync(key);
        }


        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKey">Redis Key集合</param>
        /// <returns></returns>
        public static async Task<RedisValue[]> GetStringKeyAsync(List<RedisKey> listKey)
        {
            return await db.StringGetAsync(listKey.ToArray());
        }

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> GetStringKeyAsync<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(await db.StringGetAsync(key));
        }
        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public async Task<double> StringIncrementAsync(string key, double val = 1)
        {
            return await db.StringIncrementAsync(key, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public async Task<double> StringDecrementAsync(string key, double val = 1)
        {
            return await db.StringDecrementAsync(key, val);
        }
        #endregion

        #region Hash 同步
        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool HashSet1<T>(string key, string dataKey, List<T> t)
        {
            string json = "";
            foreach (var item in t)
            {
                json = JsonConvert.SerializeObject(item);
                //listHashEntry.Add(new HashEntry(getModelId(item), json));
            }
            return db.HashSet(key, dataKey, json);

            //return await  ({
            //    string json = ConvertJson(t);
            //    return db.HashSetAsync(key, dataKey, json);
            //});
        }
        /// <summary>
        /// 保存一个集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="list">数据集合</param>
        /// <param name="getModelId"></param>
        public static void HashSet<T>(string key, List<T> list, Func<T, string> getModelId)
        {
            List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var item in list)
            {
                string json = JsonConvert.SerializeObject(item);
                listHashEntry.Add(new HashEntry(getModelId(item), json));
            }
            db.HashSet(key, listHashEntry.ToArray());
        }

        /// <summary>
        /// 获取Hash中的单个key的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="hasFildValue">RedisValue</param>
        /// <returns></returns>
        public static T GetHashKey<T>(string key, string hasFildValue)
        {
            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(hasFildValue))
            {
                RedisValue value = db.HashGet(key, hasFildValue);
                if (!value.IsNullOrEmpty)
                {
                    return JsonConvert.DeserializeObject<T>(value);
                }
            }
            return default(T);
        }

        /// <summary>
        /// 获取hash中的多个key的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="listhashFields">RedisValue value</param>
        /// <returns></returns>
        public static List<T> GetHashKey<T>(string key, List<RedisValue> listhashFields)
        {
            List<T> result = new List<T>();
            if (!string.IsNullOrWhiteSpace(key) && listhashFields.Count > 0)
            {
                RedisValue[] value = db.HashGet(key, listhashFields.ToArray());
                foreach (var item in value)
                {
                    if (!item.IsNullOrEmpty)
                    {
                        result.Add(JsonConvert.DeserializeObject<T>(item));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> GetHashAll<T>(string key)
        {
            List<T> result = new List<T>();
            RedisValue[] arr = db.HashKeys(key);
            foreach (var item in arr)
            {
                if (!item.IsNullOrEmpty)
                {
                    result.Add(JsonConvert.DeserializeObject<T>(item));
                }
            }
            return result;
        }

        /// <summary>
        /// 获取hashkey所有的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> HashGetAll<T>(string key)
        {
            List<T> result = new List<T>();
            HashEntry[] arr = db.HashGetAll(key);
            foreach (var item in arr)
            {
                if (!item.Value.IsNullOrEmpty)
                {
                    result.Add(JsonConvert.DeserializeObject<T>(item.Value));
                }
            }
            return result;
        }

        /// <summary>
        /// 删除hasekey
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static bool DeleteHase(RedisKey key, RedisValue hashField)
        {
            return db.HashDelete(key, hashField);
        }

        #endregion

        #region Hash 异步
        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static async Task<bool> HashSetAsync<T>(string key, string dataKey, List<T> t)
        {
            string json = "";
            foreach (var item in t)
            {
                json = JsonConvert.SerializeObject(item);
            }
            return await db.HashSetAsync(key, dataKey, json);

        }


        /// <summary>
        /// 获取Hash中的单个key的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="hasFildValue">RedisValue</param>
        /// <returns></returns>
        public static async Task<T> GetHashKeyAsync<T>(string key, string hasFildValue)
        {
            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(hasFildValue))
            {
                RedisValue value = await db.HashGetAsync(key, hasFildValue);
                if (!value.IsNullOrEmpty)
                {
                    return JsonConvert.DeserializeObject<T>(value);
                }
            }
            return default(T);
        }

        /// <summary>
        /// 获取hash中的多个key的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="listhashFields">RedisValue value</param>
        /// <returns></returns>
        public static async Task<List<T>> GetHashKeyAsync<T>(string key, List<RedisValue> listhashFields)
        {
            List<T> result = new List<T>();
            if (!string.IsNullOrWhiteSpace(key) && listhashFields.Count > 0)
            {
                RedisValue[] value = await db.HashGetAsync(key, listhashFields.ToArray());
                foreach (var item in value)
                {
                    if (!item.IsNullOrEmpty)
                    {
                        result.Add(JsonConvert.DeserializeObject<T>(item));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetHashAllAsync<T>(string key)
        {
            List<T> result = new List<T>();
            RedisValue[] arr = await db.HashKeysAsync(key);
            foreach (var item in arr)
            {
                if (!item.IsNullOrEmpty)
                {
                    result.Add(JsonConvert.DeserializeObject<T>(item));
                }
            }
            return result;
        }

        /// <summary>
        /// 获取hashkey所有的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<List<T>> HashGetAllAsync<T>(string key)
        {
            List<T> result = new List<T>();
            HashEntry[] arr = await db.HashGetAllAsync(key);
            foreach (var item in arr)
            {
                if (!item.Value.IsNullOrEmpty)
                {
                    result.Add(JsonConvert.DeserializeObject<T>(item.Value));
                }
            }
            return result;
        }

        /// <summary>
        /// 删除hasekey
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteHaseAsync(RedisKey key, RedisValue hashField)
        {
            return await db.HashDeleteAsync(key, hashField);
        }

        #endregion

        #region key 同步

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>是否删除成功</returns>
        public static bool KeyDelete(string key)
        {
            return db.KeyDelete(key);
        }

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">rediskey</param>
        /// <returns>成功删除的个数</returns>
        public static long keyDelete(RedisKey[] keys)
        {
            return db.KeyDelete(keys);
        }

        /// <summary>
        /// 判断key是否存储
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public static bool KeyExists(string key)
        {
            return db.KeyExists(key);
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">就的redis key</param>
        /// <param name="newKey">新的redis key</param>
        /// <returns></returns>
        public static bool KeyRename(string key, string newKey)
        {
            return db.KeyRename(key, newKey);
        }

        /// <summary>
        /// 设置Key的时间
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static bool KeyExpire(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            return db.KeyExpire(key, expiry);
        }
        #endregion

        #region key 异步

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>是否删除成功</returns>
        public static async Task<bool> KeyDeleteAsync(string key)
        {
            return await db.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">rediskey</param>
        /// <returns>成功删除的个数</returns>
        public static async Task<long> keyDeleteAsync(RedisKey[] keys)
        {
            return await db.KeyDeleteAsync(keys);
        }

        /// <summary>
        /// 判断key是否存储
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public static async Task<bool> KeyExistsAsync(string key)
        {
            return await db.KeyExistsAsync(key);
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">就的redis key</param>
        /// <param name="newKey">新的redis key</param>
        /// <returns></returns>
        public static async Task<bool> KeyRenameAsync(string key, string newKey)
        {
            return await db.KeyRenameAsync(key, newKey);
        }


        /// <summary>
        /// 设置Key的时间
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<bool> KeyExpireAsync(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            return await db.KeyExpireAsync(key, expiry);
        }
        #endregion


        #region List 同步

        /// <summary>
        /// 移除指定ListId的内部List的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void ListRemove<T>(string key, T value)
        {
            db.ListRemove(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> ListRange<T>(string key)
        {
            var values = db.ListRange(key);
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = JsonConvert.DeserializeObject<T>(item);
                result.Add(model);
            }
            return result;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void ListRightPush<T>(string key, T value)
        {
            db.ListRightPush(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string key)
        {
            var value = db.ListRightPop(key);
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            return db.ListLength(key);
        }

        #endregion 同步方法

        #region List 异步

        /// <summary>
        /// 移除指定ListId的内部List的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<long> ListRemoveAsync<T>(string key, T value)
        {
            return await db.ListRemoveAsync(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> ListRangeAsync<T>(string key)
        {
            var values = await db.ListRangeAsync(key);
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = JsonConvert.DeserializeObject<T>(item);
                result.Add(model);
            }
            return result;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<long> ListRightPushAsync<T>(string key, T value)
        {
            return await db.ListRightPushAsync(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string key)
        {
            var value = await db.ListRightPopAsync(key);
            return JsonConvert.DeserializeObject<T>(value);
        }


        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string key)
        {
            return await db.ListLengthAsync(key);
        }

        #endregion 异步方法


        #region SortedSet 同步

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        public static bool SortedSetAdd<T>(string key, T value, double score)
        {
            return db.SortedSetAdd(key, JsonConvert.SerializeObject(value), score);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static bool SortedSetRemove<T>(string key, T value)
        {
            return db.SortedSetRemove(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> SortedSetRangeByRank<T>(string key)
        {
            var values = db.SortedSetRangeByRank(key);

            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = JsonConvert.DeserializeObject<T>(item);
                result.Add(model);
            }
            return result;
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long SortedSetLength(string key)
        {
            return db.SortedSetLength(key);
        }

        #endregion 同步方法

        #region SortedSet 异步

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        public static async Task<bool> SortedSetAddAsync<T>(string key, T value, double score)
        {
            return await db.SortedSetAddAsync(key, JsonConvert.SerializeObject(value), score);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static async Task<bool> SortedSetRemoveAsync<T>(string key, T value)
        {
            return await db.SortedSetRemoveAsync(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<List<T>> SortedSetRangeByRankAsync<T>(string key)
        {
            var values = await db.SortedSetRangeByRankAsync(key);
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = JsonConvert.DeserializeObject<T>(item);
                result.Add(model);
            }
            return result;
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<long> SortedSetLengthAsync(string key)
        {
            return await db.SortedSetLengthAsync(key);
        }

        #endregion 异步方法


        /// <summary>
        /// 追加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void StringAppend(string key, string value)
        {
            ////追加值，返回追加后长度
            long appendlong = db.StringAppend(key, value);
        }

    }
}