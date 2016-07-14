using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WhamoLauncher.Charts.ViewControllers;

namespace WhamoLauncher.Charts
{
    internal static class TaskManagerWithFeedbackDialog<TController> where TController : IViewController, new() 
    {
        public static Task<TReturn> WaitUntilCompleted<TReturn>(Func<TReturn> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            var manager = new _AsyncTaskManager<TReturn>(func);

            using (var view = new TController())
            {
                Task<TReturn> task = null;
                task = manager.Start(view);
                view.ShowViewDialog();
                return task;
            }
        }

        private class _AsyncTaskManager<T>
        {
            private readonly Func<T> taskFunc;

            public _AsyncTaskManager(Func<T> func)
            {
                taskFunc = func;
            }

            public Task<T> Start(IViewController feedBackView)
            {
                var task = Task.Factory.StartNew(taskFunc);

                task.ContinueWith(t =>
                {
                    Logger.Default.Log(t.Exception);
                    feedBackView.CloseView();
                }, TaskContinuationOptions.OnlyOnFaulted);

                task.ContinueWith(t =>
                {
                    feedBackView.CloseView();
                    return t.Result;
                }, TaskContinuationOptions.NotOnFaulted);
                   
                return task;
            }
        }
    }
}
