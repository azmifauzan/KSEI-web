using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using KSEIWebKtp.Models;

namespace KSEIWebKtp.Migrations
{
    [DbContext(typeof(KseiContext))]
    partial class KseiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("KSEIWebKtp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nama");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("KSEIWebKtp.Models.Dataktp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Agama");

                    b.Property<string>("Alamat");

                    b.Property<string>("Berlaku");

                    b.Property<string>("Goldarah");

                    b.Property<string>("Jk");

                    b.Property<string>("Kecamatan");

                    b.Property<string>("KelDesa");

                    b.Property<string>("Kewarganegaraan");

                    b.Property<string>("NIK");

                    b.Property<string>("Nama");

                    b.Property<string>("Pekerjaan");

                    b.Property<string>("Provinsi");

                    b.Property<string>("RtRw");

                    b.Property<string>("Status");

                    b.Property<string>("Tanggal_lahir");

                    b.Property<string>("Tempat_lahir");

                    b.Property<int>("Upload_ID");

                    b.HasKey("ID");

                    b.ToTable("Dataktp");
                });

            modelBuilder.Entity("KSEIWebKtp.Models.Kontenws", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AGAMA");

                    b.Property<string>("ALAMAT");

                    b.Property<string>("DUSUN");

                    b.Property<string>("EKTP_CREATED");

                    b.Property<string>("EKTP_STATUS");

                    b.Property<string>("GOL_DARAH");

                    b.Property<string>("JENIS_KLMIN");

                    b.Property<string>("JENIS_PKRJN");

                    b.Property<string>("KAB_NAME");

                    b.Property<string>("KEC_NAME");

                    b.Property<string>("KEL_NAME");

                    b.Property<string>("KODE_POS");

                    b.Property<string>("NAMA_LGKP");

                    b.Property<string>("NAMA_LGKP_AYAH");

                    b.Property<string>("NAMA_LGKP_IBU");

                    b.Property<string>("NIK");

                    b.Property<string>("NO_AKTA_LHR");

                    b.Property<string>("NO_KAB");

                    b.Property<string>("NO_KEC");

                    b.Property<string>("NO_KEL");

                    b.Property<string>("NO_KK");

                    b.Property<string>("NO_PROP");

                    b.Property<string>("NO_RT");

                    b.Property<string>("NO_RW");

                    b.Property<string>("PDDK_AKH");

                    b.Property<string>("PNYDNG_CCT");

                    b.Property<string>("PROP_NAME");

                    b.Property<string>("RESPON");

                    b.Property<string>("STATUS_KAWIN");

                    b.Property<string>("TGL_LHR");

                    b.Property<string>("TMPT_LHR");

                    b.Property<int>("WebserviceID");

                    b.HasKey("ID");

                    b.HasIndex("WebserviceID");

                    b.ToTable("Kontenws");
                });

            modelBuilder.Entity("KSEIWebKtp.Models.Upload", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("File_Saved");

                    b.Property<string>("File_Upload");

                    b.Property<DateTime>("Tgl_Upload");

                    b.Property<string>("User_ID");

                    b.HasKey("ID");

                    b.ToTable("Upload");
                });

            modelBuilder.Entity("KSEIWebKtp.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Nama");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("KSEIWebKtp.Models.Webservice", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FILE_GENERATE");

                    b.Property<string>("FILE_UPLOAD");

                    b.Property<string>("PETUGAS_CEK");

                    b.Property<DateTime>("TGL_CEK");

                    b.HasKey("ID");

                    b.ToTable("Webservice");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("KSEIWebKtp.Models.Kontenws", b =>
                {
                    b.HasOne("KSEIWebKtp.Models.Webservice")
                        .WithMany("content")
                        .HasForeignKey("WebserviceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("KSEIWebKtp.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("KSEIWebKtp.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KSEIWebKtp.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
