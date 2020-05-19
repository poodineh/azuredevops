using System;
using System.Threading.Tasks;
using AzureDevOpsVariableGroups.Api;
using AzureDevOpsVariableGroups.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace AzureDevOpsVariableGroups
{
    class Program
    {
          static async Task Main (string[] args) {
            //.Net Core DI 
            var serviceprovider = new ServiceCollection ()
                .AddLogging (config => config.AddConsole ())
                .Configure<LoggerFilterOptions> (config => config.MinLevel = LogLevel.Debug)
                .AddLogging (config => config.AddSerilog ())
                .AddHttpClient ()
                .AddTransient<IAdoHttpClient, AdoHttpClient> ()
                .BuildServiceProvider ();

            Log.Logger = new LoggerConfiguration ()
                .WriteTo.File ("AzureDevOpsVariableGroups.log")
                .CreateLogger ();

            //Get Service Instances
            var adohttpclient = serviceprovider.GetRequiredService<IAdoHttpClient> ();
            var logger = serviceprovider.GetService<ILogger<Program>> ();

            var adoteamprojectslogic = new AdoTeamProjectsLogic (adohttpclient);
            IVariableGroupLogic adovariablegrouplogic = new VariableGroupLogic(adohttpclient);

            var pipelinevariablesexistinginstances = 0;
            var pipelinevariablesinstancesnewlycreated = 0;
            
            try {
                //Full Logic
                //Busines Logic Requirements: 
                //1) Get All ADO Team Projects
                logger.LogInformation ("Getting Team Projects...");
                var teamprojects = await adoteamprojectslogic.GetTeamProjects ();
                //2) For each team project, get variable group
                foreach (var teamproject in teamprojects) {
                    var entry = await adovariablegrouplogic.GetVariableGroup(teamproject.Key);
                    //3) If PipelineVariable group exits, update variable group
                    if (entry.Value) {
                        pipelinevariablesexistinginstances++;
                        await adovariablegrouplogic.UpdateVariableGroup (teamproject.Key, entry.Key);
                    } else
                    //4) If PipelineVariable group does not exist, create it 
                    {
                        pipelinevariablesinstancesnewlycreated++;
                        //Create Pipeline Variable              
                        var pipelinevariablecreateresponse = await adovariablegrouplogic.CreateVariableGroup(teamproject.Key);
                        //Authorize Newly Created Pipeline Variable group to all pipelines in the team project
                        var pipelinevariableentry = await adovariablegrouplogic.AuthorizeVariableGroup (teamproject.Key, pipelinevariablecreateresponse.Key);
                    }
                }
                //5) Log the number of service connections created
                logger.LogInformation ($"Total Team Projects Returned: {teamprojects.Count}");
                logger.LogInformation ($"Total Instances of Pipeline Variables Found and Updated: {pipelinevariablesexistinginstances}");
                logger.LogInformation ($"Total Instances of Pipeline Variables Created: {pipelinevariablesinstancesnewlycreated}");              
            } catch (System.Exception ex) {
                logger.LogError (ex, "Error Creating Pipeline Variables Connections. See Full Log for Details");
            }
        }
    }
}
