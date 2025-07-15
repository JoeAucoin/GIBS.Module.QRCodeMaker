using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.QRCodeMaker.Migrations.EntityBuilders
{
    public class QRCodeMakerEntityBuilder : AuditableBaseEntityBuilder<QRCodeMakerEntityBuilder>
    {
        private const string _entityTableName = "GIBSQRCodeMaker";
        private readonly PrimaryKey<QRCodeMakerEntityBuilder> _primaryKey = new("PK_GIBSQRCodeMaker", x => x.QRCodeMakerId);
        private readonly ForeignKey<QRCodeMakerEntityBuilder> _moduleForeignKey = new("FK_GIBSQRCodeMaker_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public QRCodeMakerEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override QRCodeMakerEntityBuilder BuildTable(ColumnsBuilder table)
        {
            QRCodeMakerId = AddAutoIncrementColumn(table,"QRCodeMakerId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            QR_CodeType = AddStringColumn(table, "QR_CodeType",100, true);
            ContentData = AddMaxStringColumn(table, "ContentData");
            Notes = AddMaxStringColumn(table, "Notes", true);
            QR_Color = AddStringColumn(table, "QR_Color", 50, true);
            QR_BackgroundColor = AddStringColumn(table, "QR_BackgroundColor", 50, true);
            QR_Logo = AddMaxStringColumn(table, "QR_Logo", true);
            ImageURL = AddMaxStringColumn(table, "ImageURL", true);


            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> QRCodeMakerId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
        public OperationBuilder<AddColumnOperation> QR_CodeType { get; set; }
        public OperationBuilder<AddColumnOperation> ContentData { get; set; }
        public OperationBuilder<AddColumnOperation> Notes { get; set; }
        public OperationBuilder<AddColumnOperation> QR_Color { get; set; }
        public OperationBuilder<AddColumnOperation> QR_BackgroundColor { get; set; }
        public OperationBuilder<AddColumnOperation> QR_Logo { get; set; }
        public OperationBuilder<AddColumnOperation> ImageURL { get; set; }

    }
}
