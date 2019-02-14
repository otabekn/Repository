using Microsoft.AspNetCore.Mvc;
using RepositoryRule.Base;
using RepositoryRule.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenericControllers
{
    public class GenericController<T, TKey> : ControllerBase
         where T : class, IEntity<TKey>
    {
        private IRepositoryBase<T, TKey> _item;
        public GenericController(IRepositoryBase<T, TKey> item)
        {
            _item = item;
        }
        [HttpGet]
        public virtual IEnumerable<T> GetAll()
        {
            return _item.FindAll();
        }
        [HttpGet]
        public virtual T Get(TKey id)
        {
            return _item.Get(id);
        }
        public virtual async Task<IEnumerable<T>> GetBy([FromBody]Query model)
        {
            if (model == null) return null;
            if (string.IsNullOrEmpty(model.key))
            {
                _item.FindReverse(model.offset, model.limit);
            }
            return await _item.FindReverseAsync(model.key, model.value, model.offset, model.limit);
            
        }
        public virtual TKey Put([FromBody]T model)
        {
            _item.Add(model);
            return model.Id;
        }
        public virtual T Delete(TKey id)
        {
          return  _item.Delete(id);
        }
    }
    public class Query
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
}
