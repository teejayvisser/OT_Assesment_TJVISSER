using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OT.Assesment.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class ADDGetTopPlayersBySpend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE PROCEDURE GetTopPlayersBySpend 
 @Count INT
AS
  BEGIN
      SELECT TOP(@Count) Sum(wagers.amount) AS totalAmountSpend,
                         playerid           AS accountId,
                         players.username
      FROM   wagers
             LEFT JOIN players
                    ON wagers.playerid = players.id
      GROUP  BY playerid,
                players.username
      ORDER  BY totalamountspend DESC
  END; 
    ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
