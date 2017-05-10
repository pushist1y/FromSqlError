using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FromSqlError
{
    public class FromSqlExample
    {

        private static void InitialSeed(CommonContext context)
        {
            var count = context.MainModels.Count();
            if (count < 200)
            {
                var nums = Enumerable.Range(2000, 100).Union(Enumerable.Range(3000, 100)); //generate records with NumData within ranges 2000..2099 and 3000..3099
                foreach (var num in nums)
                {
                    var item = new ModelMain{NumData = num};
                    context.MainModels.Add(item);
                }
                context.SaveChanges();
            }
        }
        
        public static void RunExample()
        {
            var context = new CommonContext();

            InitialSeed(context);

            //ModelFilter table is empty
            var set1 =
                context.Set<ModelFilter>() //should select 50 records
                    .FromSql(
                        "SELECT generate_series(1000000, 1000099) AS \"Id\", generate_series(1000,1099) AS \"NumData\"")
                    .Where(m => m.NumData < 1050)
                    .ToList();

            Debug.Assert(set1.Count == 50);


            var filter1 = context.Set<ModelFilter>() //should select 50 records
                .FromSql(
                    "SELECT generate_series(1000200, 1000300) AS \"Id\", generate_series(2000,2100) AS \"NumData\"")
                .Where(m => m.NumData < 2050);

            var filter2 = context.Set<ModelFilter>() //should select 50 records
                .FromSql(
                    "SELECT generate_series(1000400, 1000500) AS \"Id\", generate_series(3000,3100) AS \"NumData\"")
                .Where(m => m.NumData < 3050);


            var part1 = context.MainModels.Join(filter1, m => m.NumData, f => f.NumData, (m, f) => new {m, f}); //should produce 50 joined records
            var part2 = context.MainModels.Join(filter2, m => m.NumData, f => f.NumData, (m, f) => new { m, f });  //should produce 50 joined records

            var unionset = part1.Union(part2).ToList();

            Debug.Assert(unionset.Count == 100);


            /*
             produced queries: 

               part1: (this one is ok)

                    SELECT "m"."Id", "m"."NumData", "t"."Id", "t"."NumData"	                FROM "MainModels" AS "m"	                INNER JOIN (	                    SELECT "m1"."Id", "m1"."NumData"	                    FROM (	                        SELECT generate_series(1000200, 1000300) AS "Id", generate_series(2000,2100) AS "NumData"	                    ) AS "m1"	                    WHERE "m1"."NumData" < 2050	                ) AS "t" ON "m"."NumData" = "t"."NumData"
             
                part2: (this one is failed) 
                    SELECT "m2"."Id", "m2"."NumData", "t2"."Id", "t2"."NumData"	                FROM "MainModels" AS "m2"	                INNER JOIN (	                    SELECT "t1"."Id", "t1"."NumData"	                    FROM (	                        SELECT "m6"."Id", "m6"."NumData"               -- FromSql is ignored in this part	                        FROM "ModelFilter" AS "m6"	                    ) AS "t1"	                    WHERE "t1"."NumData" < 3050	                ) AS "t2" ON "m2"."NumData" = "t2"."NumData"



             */

        }
    }
}
