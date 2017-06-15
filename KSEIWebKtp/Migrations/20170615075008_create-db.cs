using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KSEIWebKtp.Migrations
{
    public partial class createdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    IP_ADDRESS_WS = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    Nama = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PASSWORD_WS = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    USER_WS = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dataktp",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Agama = table.Column<string>(nullable: true),
                    Alamat = table.Column<string>(nullable: true),
                    Berlaku = table.Column<string>(nullable: true),
                    Goldarah = table.Column<string>(nullable: true),
                    Jk = table.Column<string>(nullable: true),
                    Kecamatan = table.Column<string>(nullable: true),
                    KelDesa = table.Column<string>(nullable: true),
                    Kewarganegaraan = table.Column<string>(nullable: true),
                    NIK = table.Column<string>(nullable: true),
                    Nama = table.Column<string>(nullable: true),
                    Pekerjaan = table.Column<string>(nullable: true),
                    Provinsi = table.Column<string>(nullable: true),
                    RtRw = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Tanggal_lahir = table.Column<string>(nullable: true),
                    Tempat_lahir = table.Column<string>(nullable: true),
                    Upload_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dataktp", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Upload",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    File_Saved = table.Column<string>(nullable: true),
                    File_Upload = table.Column<string>(nullable: true),
                    Tgl_Upload = table.Column<DateTime>(nullable: false),
                    User_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upload", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Webservice",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FILE_GENERATE = table.Column<string>(nullable: true),
                    FILE_UPLOAD = table.Column<string>(nullable: true),
                    PETUGAS_CEK = table.Column<string>(nullable: true),
                    TGL_CEK = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Webservice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kontenws",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AGAMA = table.Column<string>(nullable: true),
                    ALAMAT = table.Column<string>(nullable: true),
                    DUSUN = table.Column<string>(nullable: true),
                    EKTP_CREATED = table.Column<string>(nullable: true),
                    EKTP_STATUS = table.Column<string>(nullable: true),
                    GOL_DARAH = table.Column<string>(nullable: true),
                    JENIS_KLMIN = table.Column<string>(nullable: true),
                    JENIS_PKRJN = table.Column<string>(nullable: true),
                    KAB_NAME = table.Column<string>(nullable: true),
                    KEC_NAME = table.Column<string>(nullable: true),
                    KEL_NAME = table.Column<string>(nullable: true),
                    KODE_POS = table.Column<string>(nullable: true),
                    NAMA_LGKP = table.Column<string>(nullable: true),
                    NAMA_LGKP_AYAH = table.Column<string>(nullable: true),
                    NAMA_LGKP_IBU = table.Column<string>(nullable: true),
                    NIK = table.Column<string>(nullable: true),
                    NO_AKTA_LHR = table.Column<string>(nullable: true),
                    NO_KAB = table.Column<string>(nullable: true),
                    NO_KEC = table.Column<string>(nullable: true),
                    NO_KEL = table.Column<string>(nullable: true),
                    NO_KK = table.Column<string>(nullable: true),
                    NO_PROP = table.Column<string>(nullable: true),
                    NO_RT = table.Column<string>(nullable: true),
                    NO_RW = table.Column<string>(nullable: true),
                    PDDK_AKH = table.Column<string>(nullable: true),
                    PNYDNG_CCT = table.Column<string>(nullable: true),
                    PROP_NAME = table.Column<string>(nullable: true),
                    RESPON = table.Column<string>(nullable: true),
                    STATUS_KAWIN = table.Column<string>(nullable: true),
                    TGL_LHR = table.Column<string>(nullable: true),
                    TMPT_LHR = table.Column<string>(nullable: true),
                    WebserviceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontenws", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Kontenws_Webservice_WebserviceID",
                        column: x => x.WebserviceID,
                        principalTable: "Webservice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kontenws_WebserviceID",
                table: "Kontenws",
                column: "WebserviceID");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dataktp");

            migrationBuilder.DropTable(
                name: "Kontenws");

            migrationBuilder.DropTable(
                name: "Upload");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Webservice");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
