using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    [DataContract(Namespace = "wadado.in")]
    public class ActivityCompany : ObjectBase
    {
        private string _ActivityCompanyKey;
        private string _ActivityKey;
        private string _CompanyKey;
        private DateTime _CreatedOn;
        private string _CreatedBy;

        public string ActivityCompanyKey
        {
            get { return _ActivityCompanyKey; }
            set
            {
                _ActivityCompanyKey = value;
                OnPropertyChanged(() => ActivityCompanyKey);
            }
        }
        public string ActivityKey
        {
            get { return _ActivityKey; }
            set
            {
                _ActivityKey = value;
                OnPropertyChanged(() => ActivityKey);
            }
        }
        public string CompanyKey
        {
            get { return _CompanyKey; }
            set
            {
                _CompanyKey = value;
                OnPropertyChanged(() => CompanyKey);
            }
        }
        public DateTime CreatedOn
        {
            get { return _CreatedOn; }
            set
            {
                _CreatedOn = value;
                OnPropertyChanged(() => CreatedOn);
            }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                OnPropertyChanged(() => CreatedBy);
            }
        }

        class ActivityCompanyValidator : AbstractValidator<ActivityCompany>
        {
            public ActivityCompanyValidator()
            {
                RuleFor(obj => obj.ActivityCompanyKey).NotNull();
                RuleFor(obj => obj.ActivityKey).NotNull();
                RuleFor(obj => obj.CompanyKey).NotNull();
                RuleFor(obj => obj.CreatedOn).NotNull();
                RuleFor(obj => obj.CreatedBy).NotEmpty();
            }
        }

        protected override IValidator GetValidator()
        {
            return new ActivityCompanyValidator();
        }
    }
}
