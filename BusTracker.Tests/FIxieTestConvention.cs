using Fixie;

namespace BusTracker.Tests
{
    public class FIxieTestConvention: Convention
    {
        public FIxieTestConvention()
        {
            Classes.NameEndsWith("Tests");

            Methods.Where(method => method.IsPublic || method.IsAsync());

            ClassExecution.CreateInstancePerClass().ShuffleCases();

            //CaseExecution Can Added the Skip Stuff here

            //Parameters()
        }
    }
}
