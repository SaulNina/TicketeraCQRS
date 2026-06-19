using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ticketera.Domain.Models.Entities;

namespace Ticketera.Infrastructure.Models;

public partial class TicketeraBdContext : DbContext
{
    public TicketeraBdContext()
    {
    }

    public TicketeraBdContext(DbContextOptions<TicketeraBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Response> Responses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Si el contexto no ha sido configurado externamente (por ejemplo, en las migraciones de consola),
        // busca la cadena de conexión por defecto.
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasKey(e => e.ResponseId).HasName("PK_responses");

            entity.ToTable("responses");

            entity.Property(e => e.ResponseId)
                .ValueGeneratedNever()
                .HasColumnName("response_id");
                
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()") // Adaptado a PostgreSQL
                .HasColumnName("created_at");
                
            entity.Property(e => e.Message)
                .HasColumnName("message"); // Removido IsUnicode (PostgreSQL maneja UTF-8 de forma nativa)
                
            entity.Property(e => e.ResponderId).HasColumnName("responder_id");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.Responder).WithMany(p => p.Responses)
                .HasForeignKey(d => d.ResponderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_responses_responder");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Responses)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK_responses_tickets");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_roles");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "UQ_roles_role_name").IsUnique();

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
                
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK_tickets");

            entity.ToTable("tickets");

            entity.Property(e => e.TicketId)
                .ValueGeneratedNever()
                .HasColumnName("ticket_id");
                
            entity.Property(e => e.ClosedAt).HasColumnName("closed_at");
            
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()") // Adaptado a PostgreSQL
                .HasColumnName("created_at");
                
            entity.Property(e => e.Description)
                .HasColumnName("description");
                
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
                
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
                
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tickets_users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_users");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ_users_email").IsUnique();

            entity.HasIndex(e => e.Username, "UQ_users_username").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
                
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()") // Adaptado a PostgreSQL
                .HasColumnName("created_at");
                
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
                
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
                
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PK_user_roles");

            entity.ToTable("user_roles");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("now()") // Adaptado a PostgreSQL
                .HasColumnName("assigned_at");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_user_roles_roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_user_roles_users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}