//
//  AppDelegate.swift
//

import UIKit

@UIApplicationMain
class AppDelegate: UIResponder, UIApplicationDelegate {
    
    var window: UIWindow?
    
    var application: UIApplication?
    
    // Get a reference for UnityAppController
    @objc var currentUnityController: UnityAppController!
    
    // Store if Onirix Unity app is running
    var isOnirixRunning = false
    
    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        
        self.application = application
        
        // Initialize the unit app.
        unity_init(CommandLine.argc, CommandLine.unsafeArgv)
        
        // Set the UnityAppController
        currentUnityController = UnityAppController()
        // Set the same application parameters that swift app
        currentUnityController.application(application, didFinishLaunchingWithOptions: launchOptions)
        
        return true
    }
    
    func applicationWillResignActive(_ application: UIApplication) {
        
        // If Onirix is running call the same life cycle state.
        if isOnirixRunning {
            currentUnityController.applicationWillResignActive(application)
        }
    }
    
    func applicationDidEnterBackground(_ application: UIApplication) {
        
        // If Onirix is running call the same life cycle state.
        if isOnirixRunning {
            currentUnityController.applicationDidEnterBackground(application)
        }
    }
    
    func applicationWillEnterForeground(_ application: UIApplication) {
        
        // If Onirix is running call the same life cycle state.
        if isOnirixRunning {
            currentUnityController.applicationWillEnterForeground(application)
        }
    }
    
    func applicationDidBecomeActive(_ application: UIApplication) {
        
        // If Onirix is running call the same life cycle state.
        if isOnirixRunning {
            currentUnityController.applicationDidBecomeActive(application)
        }
    }
    
    func applicationWillTerminate(_ application: UIApplication) {
        // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
    }
    
    // Starts Onirix app.
    func startOnirix() {
        if !isOnirixRunning
        {
            isOnirixRunning = true
            // Call Unity controller active method
            currentUnityController.applicationDidBecomeActive(application!)
        }
    }
    
    func stopOnirix() {
        if isOnirixRunning {
            // Call Unity controller resing method
            currentUnityController.applicationWillResignActive(application!)
            isOnirixRunning = false
        }
    }
    
}

