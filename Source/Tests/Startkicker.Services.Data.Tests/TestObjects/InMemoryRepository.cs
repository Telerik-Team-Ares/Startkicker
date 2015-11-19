﻿using Startkicker.Data.Models;
using Startkicker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Startkicker.Services.Data.Tests.TestObjects
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly IList<T> data;

        public InMemoryRepository()
        {
            this.data = new List<T>();
            this.AttachedEntities = new List<T>();
            this.DetachedEntities = new List<T>();
            this.UpdatedEntities = new List<T>();
        }

        //Might never need them!
        public List<T> AttachedEntities { get; private set; }

        public List<T> DetachedEntities { get; private set; }

        public IList<T> UpdatedEntities { get; private set; }

        public bool IsDisposed { get; private set; }

        public int NumberOfSaves { get; private set; }

        public void Add(T entity)
        {
            this.data.Add(entity);
        }

        public IQueryable<T> All()
        {
            return this.data.AsQueryable();
        }

        public T Attach(T entity)
        {
            this.AttachedEntities.Add(entity);
            return entity;
        }

        public void Delete(object id)
        {
            if (this.data.Count == 0)
            {
                throw new InvalidOperationException("Nothing to delete");
            }

            this.data.Remove(this.data[0]);
        }

        public void Delete(T entity)
        {
            if (!this.data.Contains(entity))
            {
                throw new InvalidOperationException("Entity not found");
            }

            this.data.Remove(entity);
        }

        public void Detach(T entity)
        {
            this.DetachedEntities.Add(entity);
        }

        public void Dispose()
        {
            this.IsDisposed = true;
        }

        public T GetById(object id)
        {
            if (this.data.Count == 0)
            {
                throw new InvalidOperationException("No objects in database");
            }

            if (id == null)
            {
                return null;
            }

            return this.data[0];
        }

        public int SaveChanges()
        {
            this.NumberOfSaves++;
            return 1;
        }

        public void Update(T entity)
        {            
            Type type = typeof(T);
            Type typeOfProject = typeof(Project);
            if (type == typeOfProject)
            {
               
            }
            this.UpdatedEntities.Add(entity);
        }
    }
}
