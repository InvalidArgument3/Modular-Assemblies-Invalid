﻿using Modular_Assemblies.Data.Scripts.AssemblyScripts.Definitions;
using Sandbox.ModAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modular_Assemblies.Data.Scripts.AssemblyScripts.DebugUtils;
using VRage.Game.Components;
using VRage.Utils;

namespace Modular_Assemblies.Data.Scripts.AssemblyScripts
{
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public class AssembliesSessionInit : MySessionComponentBase
    {
        public static AssembliesSessionInit I;
        AssemblyPartManager AssemblyPartManager = new AssemblyPartManager();
        DefinitionHandler DefinitionHandler = new DefinitionHandler();
        public static bool DebugMode = false;
        public Random Random = new Random();
        public static bool IsSessionInited = false;

        #region Base Methods

        public override void LoadData()
        {
            Stopwatch watch = Stopwatch.StartNew();
            IsSessionInited = false;

            I = this;
            ModularLog.Init();

            AssemblyPartManager.Init();
            DefinitionHandler.Init();

            CommandHandler.Init();

            watch.Stop();
            ModularLog.Log($"Fully initialized in {watch.ElapsedMilliseconds}ms.");
        }

        public override void UpdateAfterSimulation()
        {
            try
            {
                IsSessionInited = true;
                AssemblyPartManager.UpdateAfterSimulation();
            }
            catch (Exception e)
            {
                ModularLog.Log("Handled exception in Modular Assemblies!\n" + e);
            }
        }

        protected override void UnloadData()
        {
            Stopwatch watch = Stopwatch.StartNew();

            ModularLog.Log("\n===========================================\nUnload started...");

            CommandHandler.Close();

            AssemblyPartManager.Unload();
            DefinitionHandler.Unload();

            I = null;
            watch.Stop();
            ModularLog.Log($"Finished unloading in {watch.ElapsedMilliseconds}ms.");
            ModularLog.Close();
        }

        #endregion
    }
}
