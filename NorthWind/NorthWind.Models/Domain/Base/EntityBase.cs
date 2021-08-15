using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NorthWind.Models.Domain.Base
{
    public abstract class EntityBase
    {
        public EntityBase() {
            State = State.Unchanged;
        }

        // Return type T as EntityBase (in where) , new function =>
        public static T CreateInstance<T>(State state) where T : EntityBase, new() {
            T instance = new T
            {
                State = state
            };

            return instance;
        }

        public int Id { get; set; }

        [NotMapped]
        public State State { get; set; }

        public IEnumerable<ValidationResult> Validate()
        {
            throw new NotImplementedException();
        }

        //[Timestamp]
        //public byte[] TimeStamp { get; set; }
    }
}
