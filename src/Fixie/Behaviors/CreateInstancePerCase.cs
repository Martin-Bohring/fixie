﻿using System;

namespace Fixie.Behaviors
{
    public class CreateInstancePerCase : ClassBehavior
    {
        readonly Func<Type, object> construct;

        public CreateInstancePerCase(Func<Type, object> construct)
        {
            this.construct = construct;
        }

        public void Execute(ClassExecution classExecution, Action next)
        {
            foreach (var caseExecution in classExecution.CaseExecutions)
            {
                try
                {
                    var instance = construct(classExecution.TestClass);

                    var executionModel = classExecution.ExecutionModel;
                    var instanceExecution = new InstanceExecution(executionModel, classExecution.TestClass, instance, new[] { caseExecution });
                    executionModel.Execute(instanceExecution);

                    Dispose(instance);
                }
                catch (Exception exception)
                {
                    caseExecution.Fail(exception);
                }
            }
        }

        static void Dispose(object instance)
        {
            var disposable = instance as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}