using Microsoft.AspNetCore.Mvc;
using RepositoryRule.Base;
using RepositoryRule.Entity;
using System.Collections;
using System.Collections.Generic;

namespace GenericControllera
{
    public class GenericController<T, TKey>:ControllerBase
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
        public virtual IEnumerable<T> GetBy([FromBody]Query model) {
            return null;
        }
        public virtual TKey Put([FromBody]T model)
        {
            
            _item.Add(model);
            return model.Id;
        }
        public virtual bool Delete(TKey id)
        {
            return _item.Delete(id);
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
