using System.Windows;
using LuaSTGEditorSharp.Execution;

namespace LuaSTGEditorSharp
{
    public class LSTGSubExecution : LSTGExecution
    {
        public override void BeforeRun(ExecutionConfig config)
        {
            // LuaSTG Sub v0.21.118+ supports stdout redirect
            var currentApp = Application.Current as IAppDebugSettings;
            var luaSetting = "\""
                             + "start_game=true "
                             + "is_debug=true "
                             + "cheat=" + currentApp!.DebugCheat.ToString().ToLower() + " "
                             + "setting.windowed=" + currentApp!.DebugWindowed.ToString().ToLower() + " "
                             + "setting.resx=" + currentApp!.DebugResolutionX + " "
                             + "setting.resy=" + currentApp!.DebugResolutionY + " "
                             + "setting.mod=\'" + config.ModName + "\'"
                             + "\"";
            var stdout = "--logging.standard_output.enable=" + currentApp!.DynamicDebugReporting.ToString().ToLower();
            Parameter = luaSetting + " " + stdout;
            UseShellExecute = false;
            CreateNoWindow = true;
            RedirectStandardError = false;
            RedirectStandardOutput = currentApp!.DynamicDebugReporting;
        }

        protected override string LogFileName => "engine.log";
        public override string ExecutableName => "LuaSTGSub.exe";
    }
}