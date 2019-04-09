using Microsoft.EntityFrameworkCore.Migrations;

namespace RssAtomFid.Api.Migrations
{
    public partial class ConstraindUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedSources_FeedsCollections_FeedsCollectionId",
                table: "FeedSources");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedSources_FeedsCollections_FeedsCollectionId",
                table: "FeedSources",
                column: "FeedsCollectionId",
                principalTable: "FeedsCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedSources_FeedsCollections_FeedsCollectionId",
                table: "FeedSources");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedSources_FeedsCollections_FeedsCollectionId",
                table: "FeedSources",
                column: "FeedsCollectionId",
                principalTable: "FeedsCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
