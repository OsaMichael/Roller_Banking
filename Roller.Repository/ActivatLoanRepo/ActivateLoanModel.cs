using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository
{
   public class ActivateLoanModel
    {
        [Key]
        public int ActivatId { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public decimal Loan_Amount { get; set; }
       
        public decimal AmountTo_Pay { get; set; }
        public decimal Loan_Amount_Paid { get; set; }
        [Required]
        public decimal Interest_Rate { get; set; }
        public int Duration { get; set; }
        public string Frequency { get; set; }
        public int Deadline { get; set; }
        public string LoanCause { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //public virtual RatingModel Rating { get; set; }
        //public virtual ICollection<RatingModel> Ratings { get; set; }
        //public virtual ICollection<RestaurantModel> Restaurantss { get; set; }

        //public virtual RatingModel Rating { get; set; }

        //public PeriodModel()
        //{
        //    IsActive = true;

        //    new HashSet<RatingModel>();
        //    new HashSet<RestaurantModel>();

        //    //new RatingModel();
        //}
        //public PeriodModel(Period period)
        //{
        //    this.Assign(period);

        //    //Rating = new RatingModel();
        //    Ratings = new HashSet<RatingModel>();
        //    Restaurantss = new HashSet<RestaurantModel>();
        //    //Rating = new RatingModel();

        //}
        //public Period Create(PeriodModel model)
        //{
        //    return new Period
        //    {
        //        PeriodName = model.PeriodName,
        //        Discription = model.Discription,
        //        EndDate = model.EndDate,
        //        StartDate = model.StartDate,
        //        CreatedBy = model.CreatedBy,
        //        IsActive = true,
        //        //CreatedDate = model.CreatedDate,
        //        CreatedDate = DateTime.Now,
        //        //IsApplicationActive = model.IsApplicationActive

        //    };
        //}
        //public Period Edit(Period entity, PeriodModel model)
        //{

        //    entity.PeriodName = model.PeriodName;

        //    entity.ModifiedBy = model.ModifiedBy;
        //    //entity.CreatedBy = model.CreatedBy;
        //    entity.ModifiedDate = DateTime.Now;
        //    //IsApplicationActive = model.IsApplicationActive;
        //    //entity.CreatedDate = DateTime.Now;
        //    return entity;
        //}
        //public Period ActivateNow(Period myEntities, PeriodModel myModel)
        //{
        //    myEntities.Discription = myModel.Discription;
        //    myEntities.StartDate = myModel.StartDate;
        //    myEntities.EndDate = myModel.EndDate;
        //    myEntities.PeriodId = myModel.PeriodId;
        //    IsActive = true;
        //    myEntities.ModifiedBy = "Administrator";
        //    myEntities.ModifiedDate = DateTime.Now;
        //    myEntities.CreatedDate = DateTime.Now;
        //    return myEntities;
        //}
    }
}
