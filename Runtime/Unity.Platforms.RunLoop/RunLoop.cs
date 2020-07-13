namespace Unity.Platforms
{
    public class RunLoop
    {
        /// Timestamp in seconds as a double since some platform-dependent point in time, that will be used to calculate delta and elapsed time.
        /// It is expected to be a timestamp from monotonic high-frequency timer, but on some platforms it is received from a wallclock timer (emscripten, html5)
        public delegate bool RunLoopDelegate(double timestampInSeconds);
        
        public static void EnterMainLoop(RunLoopDelegate runLoopDelegate)
        {
            RunLoopImpl.EnterMainLoop(runLoopDelegate);
        }
    }
}
