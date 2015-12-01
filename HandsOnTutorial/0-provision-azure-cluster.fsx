(**

# Provisioning your cluster using F# interactive

> If using a locally simulated cluster (via ThespianCluster.fsx) you can ignore this tutorial.

> This tutorial is from the [MBrace Starter Kit](https://github.com/mbraceproject/MBrace.StarterKit).

In this tutorial you learn how you can use the [MBrace.Azure.Management](https://www.nuget.org/packages/MBrace.Azure) library
to provision an MBrace cluster using F# Interactive. In order to proceed, you will need
to [sign up](https://azure.microsoft.com/en-us/pricing/free-trial/) for an Azure subscription. Once signed up, 
[download your publication settings file ](https://manage.windowsazure.com/publishsettings).

*)

#load "AzureCluster.fsx"
open MBrace.Azure
open MBrace.Azure.Management

(**
First, let's supply three simple settings to create our cluster
*)

// You can download your publication settings file at 
//     https://manage.windowsazure.com/publishsettings
let pubSettingsFile = @"C:\path\to\your.publishsettings"

// Your prefered Azure service name for the cluster.
// NB: must be a valid DNS prefix unique across Azure.
let clusterName = "replace with a valid and unique cloud service name"

// If your publication settings defines more than one subscription,
// you will need to specify which one you will be using here. If not, you can leave this as None.
let subscriptionId = None

// Here we just bundle the configuration settings above into a triple
// so that we can more easily pass them around.
let config = pubSettingsFile, subscriptionId, clusterName

(**

Now let's create a new cluster by calling

*)

let deployment = config |> Config.ProvisionCluster(Region.North_Europe, 4, VMSize.Large)

(**

We can track the provisioning progress of the cluster by calling

*)

deployment.ShowInfo()

(**

Provisioning can take some time (approximately 5 minutes). 
Once done, you can now connect to your cluster as follows:

*)

let cluster = config |> Config.GetCluster

cluster.ShowWorkers()

(**

You can now run any of the subsequent tutorials and examples in your Azure cluster.

## Modifying the cluster

You can resize the cluster by calling

*)

config |> Config.ResizeCluster 20

(**

When done, it is important to make sure that the cluster has been deprovisioned

*)

config |> Config.DeleteCluster

(**

## Summary

In this tutorial, you've learned how to provision an MBrace clusters running on Azure using MBrace.Azure.Management.
You are now ready to proceed with further samples to learn more about the MBrace programming model.  

To learn more about provisioning, monitoring, scaling and deleting clusters using MBrace.Azure.Management
see [going-further/200-managing-azure-clusters.fsx](going-further/200-managing-azure-clusters.html).

*)