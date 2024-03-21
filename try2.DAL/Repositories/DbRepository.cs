using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using try2.DAL.Interfaces;
using try2.DAL.Models;
using try2.Domain.Entities;
using try2.Domain.Entities.Base;
using Version = try2.DAL.Models.Version;

namespace try2.DAL.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DbSet<T>? _Set;

        private readonly AirplanesDbContext _db;

        public virtual IQueryable<T> Items => _Set;


        public T Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            _db.SaveChanges();
            return item;    
        }

        public async Task<T> AddAsync(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            await _db.SaveChangesAsync();
            return item;
        }

        public T Get(long id)
        {
            return Items.FirstOrDefault(i => i.Id == id);
        }

        public async Task GetAsync(long id) => await Items.SingleOrDefaultAsync(i => i.Id == id).ConfigureAwait(false);

        public void Remove(long id)
        {
            var item = _Set.Local.FirstOrDefault(i => i.Id == id) ?? new T { Id = id };

            _db.Remove(item);

            _db.SaveChanges();
        }

        public async Task RemoveAsync(long id)
        {
            var item = await _db.Set<T>().Where(i => i.Id == id).SingleOrDefaultAsync() ?? new T { Id = id };
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }

        public void Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (!_db.Set<T>().Local.Any(e => e.Id == item.Id))
            {
                _db.Set<T>().Attach(item);
                _db.Entry(item).State = EntityState.Modified;
            }
            _db.SaveChanges();
        }

        public async Task UpdateAsync(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public DbRepository(AirplanesDbContext db)
        {
            _db = db;
            _Set = db.Set<T>();
        }
    }

    public class ProjectRepository : DbRepository<Project>
    {

        public override IQueryable<Project> Items => base.Items.Include(item => item.Versions);

        public ProjectRepository(AirplanesDbContext db) : base(db)
        {

        }
    }

    public class VersionRepository : DbRepository<Version>
    {

        public override IQueryable<Version> Items => base.Items.Include(item => item.Project);

        public VersionRepository(AirplanesDbContext db) : base(db)
        {

        }
    }

    public class EducationTypeRepository : DbRepository<EducationType>
    {

        public override IQueryable<EducationType> Items => base.Items.Include(item => item.Experts);

        public EducationTypeRepository(AirplanesDbContext db) : base(db)
        {

        }
    }

    public class ExpertRepository : DbRepository<Expert>
    {

        public override IQueryable<Expert> Items => base.Items.Include(item => item.EducationNavigation)
            //.Include(item => item.AircraftTypeForExperts)
            .Include(item => item.Examinations);

        public ExpertRepository(AirplanesDbContext db) : base(db)
        {

        }
    }


    public class AircraftTypeRepository : DbRepository<AircraftType>
    {

        public override IQueryable<AircraftType> Items => base.Items.Include(item => item.AircraftTypeForExperts);

        public AircraftTypeRepository(AirplanesDbContext db) : base(db)
        {

        }
    }

    



    public class HmiQuestionnaireRepository : DbRepository<HmiQuestionnaire>
    {

        public override IQueryable<HmiQuestionnaire> Items => base.Items
            .Include(item => item.ExaminationTemplates)
            .Include(item => item.HmiAnswers)
            .Include(item => item.HmiQuestionnareGeneralAnswers)
            .Include(item => item.HmiSectionGeneralAnswers)
            ;

        public HmiQuestionnaireRepository(AirplanesDbContext db) : base(db)
        {

        }
    }
    

        public class ImQuestionnaireRepository : DbRepository<ImQuestionnaire>
        {

        public override IQueryable<ImQuestionnaire> Items => base.Items
            .Include(item => item.ExaminationTemplates)
            .Include(item => item.ImAnswers)
            .Include(item => item.ImQuestionnareGeneralAnswers)
            .Include(item => item.ImSectionGeneralAnswers)
            ;

        public ImQuestionnaireRepository(AirplanesDbContext db) : base(db)
        {

        }
    }

    public class ExaminationTemplateRepository : DbRepository<ExaminationTemplate>
    {

        public override IQueryable<ExaminationTemplate> Items => base.Items
            .Include(item => item.Examinations)
            .Include(item => item.QrhmiNavigation)
            .Include(item => item.QrimNavigation)
            ;

        public ExaminationTemplateRepository(AirplanesDbContext db) : base(db)
        {

        }
    }

    //8
    public class ExaminationRepository : DbRepository<Examination>
    {

        public override IQueryable<Examination> Items => base.Items
            .Include(item => item.ExaminationTemplate)
            .Include(item => item.Expert)
            .Include(item => item.HmiAnswers)
            .Include(item => item.HmiQuestionnareGeneralAnswers)
            .Include(item => item.HmiSectionGeneralAnswers)
            .Include(item => item.ImAnswers)
            .Include(item => item.ImQuestionnareGeneralAnswers)
            .Include(item => item.ImSectionGeneralAnswers)
            .Include(item => item.Version);

        public ExaminationRepository(AirplanesDbContext db) : base(db)
        {

        }
    }


    public class ImSectionGeneralAnswerRepository : DbRepository<ImSectionGeneralAnswer>
    {

        public override IQueryable<ImSectionGeneralAnswer> Items => base.Items
            .Include(item => item.Examination)
            .Include(item => item.Questionnaire)
            .Include(item => item.Section);

        public ImSectionGeneralAnswerRepository(AirplanesDbContext db) : base(db)
        {

        }
    }



    public class ImQuestionnareGeneralAnswerRepository : DbRepository<ImQuestionnareGeneralAnswer>
    {

        public override IQueryable<ImQuestionnareGeneralAnswer> Items => base.Items
            .Include(item => item.Examination)
            .Include(item => item.Questionnaire);

        public ImQuestionnareGeneralAnswerRepository(AirplanesDbContext db) : base(db)
        {

        }
    }



    public class HmiQuestionnareGeneralAnswerRepository : DbRepository<HmiQuestionnareGeneralAnswer>
    {

        public override IQueryable<HmiQuestionnareGeneralAnswer> Items => base.Items
            .Include(item => item.Examination)
            
            .Include(item => item.Questionnaire);

        public HmiQuestionnareGeneralAnswerRepository(AirplanesDbContext db) : base(db)
        {

        }
    }



    public class ImGroupRequestRepository : DbRepository<ImGroupRequest>
    {

        public override IQueryable<ImGroupRequest> Items => base.Items
            .Include(item => item.ImRequests)
            .Include(item => item.InverseParent)
            .Include(item => item.Parent)
            ;

        public ImGroupRequestRepository(AirplanesDbContext db) : base(db)
        {

        }
    }

    public class ImRequestRepository : DbRepository<ImRequest>
    {

        public override IQueryable<ImRequest> Items => base.Items
            .Include(item => item.Group)
            .Include(item => item.ImAnswers)
            ;

        public ImRequestRepository(AirplanesDbContext db) : base(db)
        {

        }
    }



    public class HmiGroupRequestRepository : DbRepository<HmiGroupRequest>
    {

        public override IQueryable<HmiGroupRequest> Items => base.Items
            .Include(item => item.HmiRequests)
            .Include(item => item.InverseParent)
            .Include(item => item.Parent)
            ;

        public HmiGroupRequestRepository(AirplanesDbContext db) : base(db)
        {

        }
    }



    public class HmiRequestRepository : DbRepository<HmiRequest>
    {

        public override IQueryable<HmiRequest> Items => base.Items
            .Include(item => item.Group)
            .Include(item => item.HmiAnswers)
            ;

        public HmiRequestRepository(AirplanesDbContext db) : base(db)
        {

        }
    }

    public class ImSectionRepository : DbRepository<ImSection>
    {

        public override IQueryable<ImSection> Items => base.Items
            .Include(item => item.ImAnswers)
            .Include(item => item.ImSectionGeneralAnswers)
            ;

        public ImSectionRepository(AirplanesDbContext db) : base(db)
        {

        }
    }


    public class HmiSectionRepository : DbRepository<HmiSection>
    {

        public override IQueryable<HmiSection> Items => base.Items
            .Include(item => item.HmiAnswers)
            .Include(item => item.HmiSectionGeneralAnswers)
            ;

        public HmiSectionRepository(AirplanesDbContext db) : base(db)
        {

        }
    }

    public class HmiSectionGeneralAnswerRepository : DbRepository<HmiSectionGeneralAnswer>
    {

        public override IQueryable<HmiSectionGeneralAnswer> Items => base.Items
            .Include(item => item.Examination)
            .Include(item => item.Questionnaire)
            .Include(item => item.Section)
            ;

        public HmiSectionGeneralAnswerRepository(AirplanesDbContext db) : base(db)
        {

        }
    }

    public class ImAnswerRepository : DbRepository<ImAnswer>
    {

        public override IQueryable<ImAnswer> Items => base.Items
            .Include(item => item.Examination)
            .Include(item => item.Questionnaire)
            .Include(item => item.Request)
            .Include(item => item.Section)
            ;

        public ImAnswerRepository(AirplanesDbContext db) : base(db)
        {

        }
    }



    //19
    public class HmiAnswerRepository : DbRepository<HmiAnswer>
    {

        public override IQueryable<HmiAnswer> Items => base.Items
            .Include(item => item.Examination)
            .Include(item => item.Questionnaire)
            .Include(item => item.Request)
            .Include(item => item.Section)
            ;

        public HmiAnswerRepository(AirplanesDbContext db) : base(db)
        {

        }
    }






    /*  public class UserRepository : DbRepository<User>
      {

          public override IQueryable<User> Items => base.Items.Include(item => item.Profiles);

          public UserRepository(AccountDbContext db) : base(db)
          {


          }
      }

      public class ProfileRepository : DbRepository<Profile>
      {

          public override IQueryable<Profile> Items => base.Items.Include(item => item.ThisUser);

          public ProfileRepository(AccountDbContext db) : base(db)
          {


          }
      }
    */
}
