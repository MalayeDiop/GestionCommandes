using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestionCommandes.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "livreur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: false),
                    Telephone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livreur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "produit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Libelle = table.Column<string>(type: "text", nullable: false),
                    PrixUnitaire = table.Column<int>(type: "integer", nullable: false),
                    QteStock = table.Column<double>(type: "double precision", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: false),
                    Telephone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "commande",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Montant = table.Column<double>(type: "double precision", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    LivreurId = table.Column<int>(type: "integer", nullable: false),
                    EtatCom = table.Column<int>(type: "integer", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commande", x => x.Id);
                    table.ForeignKey(
                        name: "FK_commande_livreur_LivreurId",
                        column: x => x.LivreurId,
                        principalTable: "livreur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Solde = table.Column<double>(type: "double precision", nullable: false),
                    Adresse = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detailCommande",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QteCom = table.Column<double>(type: "double precision", nullable: false),
                    CommandeId = table.Column<int>(type: "integer", nullable: false),
                    PrixTotal = table.Column<double>(type: "double precision", nullable: false),
                    ProduitId = table.Column<int>(type: "integer", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detailCommande", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detailCommande_commande_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "commande",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detailCommande_produit_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "produit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "paiement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypePaiement = table.Column<int>(type: "integer", nullable: false),
                    CommandeId = table.Column<int>(type: "integer", nullable: false),
                    HasReduction = table.Column<bool>(type: "boolean", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paiement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_paiement_commande_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "commande",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_client_UserId",
                table: "client",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_commande_LivreurId",
                table: "commande",
                column: "LivreurId");

            migrationBuilder.CreateIndex(
                name: "IX_detailCommande_CommandeId",
                table: "detailCommande",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_detailCommande_ProduitId",
                table: "detailCommande",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_paiement_CommandeId",
                table: "paiement",
                column: "CommandeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "detailCommande");

            migrationBuilder.DropTable(
                name: "paiement");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "produit");

            migrationBuilder.DropTable(
                name: "commande");

            migrationBuilder.DropTable(
                name: "livreur");
        }
    }
}
