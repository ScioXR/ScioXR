//put into Assets/Plugins/WebGL/HandleIO.jslib
 var HandleIO = {
     WindowAlert : function(message)
     {
         window.alert(Pointer_stringify(message));
     },
     SyncFiles : function()
     {
         FS.syncfs(false,function (err) {
             // handle callback
			 console.log("JS: FileSync completed");
         });
     }
 };
 
 mergeInto(LibraryManager.library, HandleIO);