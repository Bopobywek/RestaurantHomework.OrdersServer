using FluentMigrator;

namespace RestaurantHomework.Authorization.Dal.Migrations;

[Migration(202305281414, TransactionBehavior.None)]
public class AddDishTypeV1 : Migration {
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'dishes_v1') THEN
            CREATE TYPE dishes_v1 as
            (
                  id            int
                , name          varchar(100)
                , description   text
                , price         decimal(10, 2)
                , quantity      int
                , is_available  boolean
                , created_at    timestamp
                , updated_at    timestamp
            );
        END IF;
    END
$$;";

        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS dishes_v1;
    END
$$;";

        Execute.Sql(sql);
    }
}