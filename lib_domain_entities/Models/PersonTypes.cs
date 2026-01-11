using lib_utilities.Utilities;
using lib_domain_context;
using System.ComponentModel.DataAnnotations;

namespace lib_domain_entities.Models
{
    public partial class PersonTypes : Bindable, IEntities
    {
        private int _Id;
        [Key] public virtual int Id
        {
            get { return _Id; }
            set { SetProperty(ref _Id, value); }
        }

        private string? _Name;
        public virtual string? Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }

        public virtual int Get_Id() { return Id; }
        public virtual object GetClone() { return this.MemberwiseClone(); }
    }
}