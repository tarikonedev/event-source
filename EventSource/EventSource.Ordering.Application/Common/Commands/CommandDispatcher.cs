using System.Reflection;

namespace EventSource.Ordering.Application.Common.Commands
{
    public class CommandDispatcher
    {
        public async Task Dispatch<TCommand>(TCommand command) where TCommand : class
        {
            //derive a type based on the ICommand interface and the generic method argument
            Type handler = typeof(ICommandHandler<>);
            Type handlerType = handler.MakeGenericType(command.GetType());

            //Find any concrete classes that implements the interface ICommandHandler<TCommand>
            Type[] concreteTypes = Assembly.GetExecutingAssembly().GetTypes()
                                    .Where(t => t.IsClass && t.GetInterfaces().Contains(handlerType))
                                    .ToArray();

            // Invoke “Handle” on the concrete handler class
            if (!concreteTypes.Any()) return;

            foreach (Type type in concreteTypes)
            {
                var concreteHandler = Activator.CreateInstance(type) as ICommandHandler<TCommand>;
                await concreteHandler?.Handle(command);
            }
        }
    }
}
