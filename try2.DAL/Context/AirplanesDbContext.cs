using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace try2.DAL.Models;

public partial class AirplanesDbContext : DbContext
{
    public AirplanesDbContext()
    {
    }

    public AirplanesDbContext(DbContextOptions<AirplanesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AircraftType> AircraftTypes { get; set; }

    public virtual DbSet<AircraftTypeForExpert> AircraftTypeForExperts { get; set; } //

    public virtual DbSet<EducationType> EducationTypes { get; set; }

    public virtual DbSet<Examination> Examinations { get; set; }

    public virtual DbSet<ExaminationTemplate> ExaminationTemplates { get; set; }

    public virtual DbSet<Expert> Experts { get; set; }

    public virtual DbSet<HmiAnswer> HmiAnswers { get; set; }

    public virtual DbSet<HmiGroupRequest> HmiGroupRequests { get; set; }

    public virtual DbSet<HmiQuestionnaire> HmiQuestionnaires { get; set; }

    public virtual DbSet<HmiQuestionnareGeneralAnswer> HmiQuestionnareGeneralAnswers { get; set; }

    public virtual DbSet<HmiRequest> HmiRequests { get; set; }

    public virtual DbSet<HmiRequestInSection> HmiRequestInSections { get; set; } //

    public virtual DbSet<HmiSection> HmiSections { get; set; }

    public virtual DbSet<HmiSectionGeneralAnswer> HmiSectionGeneralAnswers { get; set; }

    public virtual DbSet<HmiSectionInQuestionnaire> HmiSectionInQuestionnaires { get; set; } //

    public virtual DbSet<ImAnswer> ImAnswers { get; set; }

    public virtual DbSet<ImGroupRequest> ImGroupRequests { get; set; } 
    
    public virtual DbSet<ImQuestionnaire> ImQuestionnaires { get; set; }

    public virtual DbSet<ImQuestionnareGeneralAnswer> ImQuestionnareGeneralAnswers { get; set; }

    public virtual DbSet<ImRequest> ImRequests { get; set; } 

    public virtual DbSet<ImRequestInSection> ImRequestInSections { get; set; } //

    public virtual DbSet<ImSection> ImSections { get; set; }

    public virtual DbSet<ImSectionGeneralAnswer> ImSectionGeneralAnswers { get; set; }

    public virtual DbSet<ImSectionInQuestionnaire> ImSectionInQuestionnaires { get; set; } //

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Version> Versions { get; set; }

    //26 xd

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=airplanesDB;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("analysis_type", new[] { "hmi", "im" })
            .HasPostgresEnum("quality", new[] { "like_high", "like_mid", "like_low", "neutral", "dislike_high", "dislike_mid", "dislike_low" });

        modelBuilder.Entity<AircraftType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("aircraft_type_pkey");

            entity.ToTable("aircraft_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AircraftTypeForExpert>(entity =>
        {
            entity.HasKey(e => new { e.AircraftTypeId, e.ExpertId });

            entity.ToTable("aircraft_type_for_experts");

            entity.Property(e => e.AircraftTypeId).HasColumnName("aircraft_type_id");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");

            entity.HasOne(d => d.AircraftType)
                .WithMany(d => d.AircraftTypeForExperts)
                .HasForeignKey(d => d.AircraftTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("aircraft_fk");

            entity.HasOne(ate => ate.Expert)
                  .WithMany(ate => ate.AircraftTypeForExperts)
                  .HasForeignKey(ate => ate.ExpertId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("expert_fk");
        });

        modelBuilder.Entity<EducationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("education_types_pkey");

            entity.ToTable("education_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Examination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("examination_pkey");

            entity.ToTable("examination");

            entity.HasIndex(e => e.ExaminationTemplateId, "fki_exam_template_fk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ExaminationTemplateId).HasColumnName("examination_template_id");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.VersionId).HasColumnName("version_id");

            entity.HasOne(d => d.ExaminationTemplate).WithMany(p => p.Examinations)
                .HasForeignKey(d => d.ExaminationTemplateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("exam_template_fk");

            entity.HasOne(d => d.Expert).WithMany(p => p.Examinations)
                .HasForeignKey(d => d.ExpertId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("expert_fk");

            entity.HasOne(d => d.Version).WithMany(p => p.Examinations)
                .HasForeignKey(d => d.VersionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("version_fk");
        });

        modelBuilder.Entity<ExaminationTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("examination_template_pkey");

            entity.ToTable("examination_template");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descr).HasColumnName("descr");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Qrhmi).HasColumnName("qrhmi");
            entity.Property(e => e.Qrim).HasColumnName("qrim");

            entity.HasOne(d => d.QrhmiNavigation).WithMany(p => p.ExaminationTemplates)
                .HasForeignKey(d => d.Qrhmi)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("hmi_fk");

            entity.HasOne(d => d.QrimNavigation).WithMany(p => p.ExaminationTemplates)
                .HasForeignKey(d => d.Qrim)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("im_fk");
        });

        modelBuilder.Entity<Expert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("experts_pkey");

            entity.ToTable("experts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BirthYear).HasColumnName("birth_year");
            entity.Property(e => e.Education).HasColumnName("education");
            entity.Property(e => e.FlightHours).HasColumnName("flight_hours");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("patronymic");
            entity.Property(e => e.PilotClass).HasColumnName("pilot_class");
            entity.Property(e => e.ServiceYear).HasColumnName("service_year");
            entity.Property(e => e.Surname)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("surname");

            entity.HasOne(d => d.EducationNavigation).WithMany(p => p.Experts)
                .HasForeignKey(d => d.Education)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("education_fk");

        });

        modelBuilder.Entity<HmiAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hmi_answer_pkey");

            entity.ToTable("hmi_answer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.ExaminationId).HasColumnName("examination_id");
            entity.Property(e => e.Numeric).HasColumnName("numeric");
            entity.Property(e => e.QuestionnaireId).HasColumnName("questionnaire_id");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.SectionId).HasColumnName("section_id");

            entity.HasOne(d => d.Examination).WithMany(p => p.HmiAnswers)
                .HasForeignKey(d => d.ExaminationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("exam_fk");

            entity.HasOne(d => d.Questionnaire).WithMany(p => p.HmiAnswers)
                .HasForeignKey(d => d.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("questionnaire_fk");

            entity.HasOne(d => d.Request).WithMany(p => p.HmiAnswers)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("request_fk");

            entity.HasOne(d => d.Section).WithMany(p => p.HmiAnswers)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("section_fk");
        });

        modelBuilder.Entity<HmiGroupRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hmi_group_request_pkey");

            entity.ToTable("hmi_group_request");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrderNumber).HasColumnName("order_number");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("parent_fk");
        });

        modelBuilder.Entity<HmiQuestionnaire>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hmi_questionnaire_pkey");

            entity.ToTable("hmi_questionnaire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GeneralRequest).HasColumnName("general_request");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<HmiQuestionnareGeneralAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hmi_questionnare_general_answer_pkey");

            entity.ToTable("hmi_questionnare_general_answer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.ExaminationId).HasColumnName("examination_id");
            entity.Property(e => e.Numeric).HasColumnName("numeric");
            entity.Property(e => e.QuestionnaireId).HasColumnName("questionnaire_id");

            entity.HasOne(d => d.Examination).WithMany(p => p.HmiQuestionnareGeneralAnswers)
                .HasForeignKey(d => d.ExaminationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("exam_fk");

            entity.HasOne(d => d.Questionnaire).WithMany(p => p.HmiQuestionnareGeneralAnswers)
                .HasForeignKey(d => d.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("questionnaire_fk");
        });

        modelBuilder.Entity<HmiRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hmi_request_pkey");

            entity.ToTable("hmi_request");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrderNumber).HasColumnName("order_number");

            entity.HasOne(d => d.Group).WithMany(p => p.HmiRequests)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("group_fk");
        });

        modelBuilder.Entity<HmiRequestInSection>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("hmi_request_in_section");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.SectionId).HasColumnName("section_id");

            entity.HasOne(d => d.Request).WithMany()
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("req_fk");

            entity.HasOne(d => d.Section).WithMany()
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sect_fk");
        });

        modelBuilder.Entity<HmiSection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hmi_section_pkey");

            entity.ToTable("hmi_section");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GeneralRequest).HasColumnName("general_request");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<HmiSectionGeneralAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hmi_section_general_answer_pkey");

            entity.ToTable("hmi_section_general_answer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.ExaminationId).HasColumnName("examination_id");
            entity.Property(e => e.Numeric).HasColumnName("numeric");
            entity.Property(e => e.QuestionnaireId).HasColumnName("questionnaire_id");
            entity.Property(e => e.SectionId).HasColumnName("section_id");

            entity.HasOne(d => d.Examination).WithMany(p => p.HmiSectionGeneralAnswers)
                .HasForeignKey(d => d.ExaminationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("exam_fk");

            entity.HasOne(d => d.Questionnaire).WithMany(p => p.HmiSectionGeneralAnswers)
                .HasForeignKey(d => d.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("questionnaire_fk");

            entity.HasOne(d => d.Section).WithMany(p => p.HmiSectionGeneralAnswers)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("section_fk");
        });

        modelBuilder.Entity<HmiSectionInQuestionnaire>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("hmi_section_in_questionnaire");

            entity.HasIndex(e => new { e.SectionId, e.QuestionnaireId }, "hmi_sect_in_quest_uniq1").IsUnique();

            entity.Property(e => e.QuestionnaireId).HasColumnName("questionnaire_id");
            entity.Property(e => e.SectionId).HasColumnName("section_id");

            entity.HasOne(d => d.Questionnaire).WithMany()
                .HasForeignKey(d => d.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("quest_id");

            entity.HasOne(d => d.Section).WithMany()
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sect_fk");
        });

        modelBuilder.Entity<ImAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("im_answer_pkey");

            entity.ToTable("im_answer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.ExaminationId).HasColumnName("examination_id");
            entity.Property(e => e.Numeric).HasColumnName("numeric");
            entity.Property(e => e.QuestionnaireId).HasColumnName("questionnaire_id");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.SectionId).HasColumnName("section_id");

            entity.HasOne(d => d.Examination).WithMany(p => p.ImAnswers)
                .HasForeignKey(d => d.ExaminationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("exam_fk");

            entity.HasOne(d => d.Questionnaire).WithMany(p => p.ImAnswers)
                .HasForeignKey(d => d.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("questionnaire_fk");

            entity.HasOne(d => d.Request).WithMany(p => p.ImAnswers)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("request_fk");

            entity.HasOne(d => d.Section).WithMany(p => p.ImAnswers)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("section_fk");
        });

        modelBuilder.Entity<ImGroupRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("im_group_request_pkey");

            entity.ToTable("im_group_request");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrderNumber).HasColumnName("order_number");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("parent_fk");
        });

        modelBuilder.Entity<ImQuestionnaire>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("im_questionnaire_pkey");

            entity.ToTable("im_questionnaire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GeneralRequest).HasColumnName("general_request");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<ImQuestionnareGeneralAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("im_questionnare_general_answer_pkey");

            entity.ToTable("im_questionnare_general_answer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.ExaminationId).HasColumnName("examination_id");
            entity.Property(e => e.Numeric).HasColumnName("numeric");
            entity.Property(e => e.QuestionnaireId).HasColumnName("questionnaire_id");

            entity.HasOne(d => d.Examination).WithMany(p => p.ImQuestionnareGeneralAnswers)
                .HasForeignKey(d => d.ExaminationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("exam_fk");

            entity.HasOne(d => d.Questionnaire).WithMany(p => p.ImQuestionnareGeneralAnswers)
                .HasForeignKey(d => d.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("questionnaire_fk");
        });

        modelBuilder.Entity<ImRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("im_request_pkey");

            entity.ToTable("im_request");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OrderNumber).HasColumnName("order_number");

            entity.HasOne(d => d.Group).WithMany(p => p.ImRequests)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("group_fk");
        });

        modelBuilder.Entity<ImRequestInSection>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("im_request_in_section");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.SectionId).HasColumnName("section_id");

            entity.HasOne(d => d.Request).WithMany()
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("req_fk");

            entity.HasOne(d => d.Section).WithMany()
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sect_fk");
        });

        modelBuilder.Entity<ImSection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("im_section_pkey");

            entity.ToTable("im_section");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GeneralRequest).HasColumnName("general_request");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<ImSectionGeneralAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("im_section_general_answer_pkey");

            entity.ToTable("im_section_general_answer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.ExaminationId).HasColumnName("examination_id");
            entity.Property(e => e.Numeric).HasColumnName("numeric");
            entity.Property(e => e.QuestionnaireId).HasColumnName("questionnaire_id");
            entity.Property(e => e.SectionId).HasColumnName("section_id");

            entity.HasOne(d => d.Examination).WithMany(p => p.ImSectionGeneralAnswers)
                .HasForeignKey(d => d.ExaminationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("exam_fk");

            entity.HasOne(d => d.Questionnaire).WithMany(p => p.ImSectionGeneralAnswers)
                .HasForeignKey(d => d.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("questionnaire_fk");

            entity.HasOne(d => d.Section).WithMany(p => p.ImSectionGeneralAnswers)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("section_fk");
        });

        modelBuilder.Entity<ImSectionInQuestionnaire>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("im_section_in_questionnaire");

            entity.HasIndex(e => new { e.SectionId, e.QuestionnaireId }, "im_sect_in_quest_uniq1").IsUnique();

            entity.Property(e => e.QuestionnaireId).HasColumnName("questionnaire_id");
            entity.Property(e => e.SectionId).HasColumnName("section_id");

            entity.HasOne(d => d.Questionnaire).WithMany()
                .HasForeignKey(d => d.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("quest_id");

            entity.HasOne(d => d.Section).WithMany()
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sect_fk");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("projects_pkey");

            entity.ToTable("projects");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Version>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("versions_pkey");

            entity.ToTable("versions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descr).HasColumnName("descr");
            entity.Property(e => e.N).HasColumnName("n");
            entity.Property(e => e.Nn).HasColumnName("nn");
            entity.Property(e => e.Nnn).HasColumnName("nnn");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");

            entity.HasOne(d => d.Project).WithMany(p => p.Versions)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("project_fk");
        });
        modelBuilder.HasSequence("examination_template_id_seq");
        modelBuilder.HasSequence("hmi_answer_id_seq");
        modelBuilder.HasSequence("hmi_group_request_id_seq");
        modelBuilder.HasSequence("hmi_questionnaire_id_seq");
        modelBuilder.HasSequence("hmi_questionnare_general_answer_id_seq");
        modelBuilder.HasSequence("hmi_request_id_seq");
        modelBuilder.HasSequence("hmi_section_general_answer_id_seq");
        modelBuilder.HasSequence("hmi_section_id_seq");
        modelBuilder.HasSequence("im_answer_id_seq");
        modelBuilder.HasSequence("im_group_request_id_seq");
        modelBuilder.HasSequence("im_questionnaire_id_seq");
        modelBuilder.HasSequence("im_questionnare_general_answer_id_seq");
        modelBuilder.HasSequence("im_request_id_seq");
        modelBuilder.HasSequence("im_section_general_answer_id_seq");
        modelBuilder.HasSequence("im_section_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
