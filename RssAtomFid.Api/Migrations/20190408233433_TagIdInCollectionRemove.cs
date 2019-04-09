using Microsoft.EntityFrameworkCore.Migrations;

namespace RssAtomFid.Api.Migrations
{
    public partial class TagIdInCollectionRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagId",
                table: "FeedsCollections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "FeedsCollections",
                nullable: false,
                defaultValue: 0);
        }
    }
}
