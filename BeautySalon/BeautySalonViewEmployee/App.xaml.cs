using BeautySalonBusinessLogic.BusinessLogics;
using BeautySalonBusinessLogic.OfficePackage;
using BeautySalonBusinessLogic.OfficePackage.Implements;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.StoragesContracts;
using BeautySalonContracts.ViewModels;
using BeautySalonDatabaseImplement.Implements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace BeautySalonViewEmployee
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer container = null;
        public static EmployeeViewModel Employee;
        
       
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AuthorizationWindow authorizationWindow = Container.Resolve<AuthorizationWindow>();
            authorizationWindow.ShowDialog();
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IClientStorage, ClientStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IServiceStorage, ServiceStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProcedureStorage, ProcedureStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICosmeticStorage, CosmeticStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IEmployeeStorage, EmployeeStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IEstimateStorage, EstimateStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILaborCostStorage, LaborCostStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientLogic, ClientLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IServiceLogic, ServiceLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProcedureLogic, ProcedureLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICosmeticLogic, CosmeticLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IEmployeeLogic, EmployeeLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IEstimateLogic, EstimateLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILaborCostLogic, LaborCostLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportLogic, ReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToExcel, SaveToExcel>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToWord, SaveToWord>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToPdf, SaveToPdf>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
