/*
 * AlertWrapper is a static class created so that there is no need of
 * 1.creating object of class Alert in every class wherever we want to print message on console
 * 2.When setting mockAlerter no need to call that method for every Alert Object,simply can do by AlertWrapper.WhenSetAlerterMock()
 */
namespace EnvironmentMonitor
{
    public static class AlertWrapper
    {
        public static Alert AlertStaticObj;

        static AlertWrapper()
        {
            AlertStaticObj=new Alert();
        }


        public static void PrintOnConsole(string message)
        {
            AlertStaticObj.PrintOnConsole(message);
        }
        //when to send output to x unit test then MockAlerter is used
        public static void WhenSetAlerterMock()
        {
            AlertStaticObj = new AlertMock();
        }
    }
}
