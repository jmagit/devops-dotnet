using GildedRoseKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades {
    public interface IRepsitory<E, K> {
        List<E> GetAll();
        List<E> GetBy(Func<E, bool> predicate);
        E? GetById(K id);
        E Add(E item);
        E Modify(E item);
        void Delete(E item);
        void DeleteById(K id);

    }

    public interface IPersonasRepository : IRepsitory<Persona, int> { }

    public interface IDomainService<E, K> {
        List<E> GetAll();
        List<E> GetBy(Func<E, bool> predicate);
        E? GetById(K id);
        E Add(E item);
        E Modify(E item);
        void Delete(E item);
        void DeleteById(K id);

    }

    public interface IPersonasService : IDomainService<Persona, int> { }

    public class PersonasService : IPersonasService {
        private readonly IPersonasRepository dao;
        public PersonasService(IPersonasRepository dao) {
            this.dao = dao;
        }


        public List<Persona> GetAll() {
            return dao.GetAll();
        }

        public List<Persona> GetBy(Func<Persona, bool> predicate) {
            ArgumentNullException.ThrowIfNull(predicate);
            return dao.GetBy(predicate);
        }

        public Persona? GetById(int id) {
            return dao.GetById(id);
        }

        public Persona Add(Persona item) {
            if(item == null) throw new ArgumentNullException(nameof(item));
            if(!item.IsValid())  throw new ArgumentException("Datos inválidos");
            if(GetById(item.Id) != null) throw new ArgumentException("Ya existe");
            try {
            return dao.Add(item);
            } catch(Exception ex) {
                if(ex.Message.StartsWith("UNIQUE CONTRAINS"))
                    throw new ArgumentException("Datos inválidos", ex);
                throw;
            }
        }

        public Persona Modify(Persona item) {
            ArgumentNullException.ThrowIfNull(item);
            if(!item.IsValid()) throw new ArgumentException("Datos inválidos");
            if(GetById(item.Id) == null) throw new ArgumentException("No existe");
            try {
                return dao.Modify(item);
            } catch(Exception ex) {
                if(ex.Message.StartsWith("UNIQUE CONTRAINS"))
                    throw new ArgumentException("Datos inválidos", ex);
                throw;
            }
        }
        public void Delete(Persona item) {
            dao.Delete(item);
        }

        public void DeleteById(int id) {
            dao.DeleteById(id);
        }
    }
}
