mergeInto(LibraryManager.library, {
    quitUnityApp: function() {
        console.log("_j quitUnityApp has been called");
        const unityInstance = window.unityInstance;
        if (unityInstance) {
            unityInstance.Quit().then(() => {
                console.log('_j quit from jslib');
            });
        }
    }
});
