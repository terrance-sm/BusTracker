using Fixie;

namespace BusTracker.FixieTests
{
    public class TestingConvention: Convention
    {
        public TestingConvention()
        {
            Classes.NameEndsWith("Tests");

            Methods.Where(method => method.IsPublic || method.IsAsync());

            ClassExecution.CreateInstancePerClass().ShuffleCases();

            //CaseExecution Can Added the Skip Stuff here

            //Parameters()
        }
    }
}
