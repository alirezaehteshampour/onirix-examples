//
//  OnirixController.swift
//  Created by Victor Fernandez on 28/02/2016.
//

import UIKit

class OnirixController: UIViewController
{
    @IBOutlet var rotateSwitch: UISwitch!
    
    // When the controller appers, launch Onirix
    override func viewDidAppear(_ animated: Bool)
    {
        super.viewDidAppear(animated)
        loadOnirixView()
    }
    
    // When the controller disappear, remove Onirix
    override func viewWillDisappear(_ animated: Bool)
    {
        removeOnirixView()
        super.viewWillDisappear(animated)
    }
    
    // Handle 'UnityReady' event fired after first Unity load.
    @objc func handleUnityReady() {
        showUnitySubView()
    }
    
    // Get the Unity view and insert it as a subview
    func showUnitySubView() {
        if let unityView = UnityGetGLView() {
            view?.insertSubview(unityView, at: 0)
        }
    }
    
    // load the Onirix Unity imported app.
    func loadOnirixView()
    {
        // Set unityView as visible
        if let unityView = UnityGetGLView()
        {
            unityView.isHidden = false
        }
        
        // get AppDelegate reference
        if let appDelegate = UIApplication.shared.delegate as? AppDelegate
        {
            // Launch Onirix
            appDelegate.startOnirix()
            
            // Set an over for 'UnityReady' event
            NotificationCenter.default.addObserver(self, selector: #selector(handleUnityReady), name: NSNotification.Name("UnityReady"), object: nil)
        }
    }
    
    // remove the Onirix Unity imported app.
    func removeOnirixView()
    {
        // get AppDelegate reference
        if let appDelegate = UIApplication.shared.delegate as? AppDelegate
        {
            // Stop Onirix Unity app
            appDelegate.stopOnirix()

            // Remove 'UnityReady' observable
            NotificationCenter.default.removeObserver(self, name: NSNotification.Name("UnityReady"), object: nil)
            
            // Removes UnityView
            if let unityView = UnityGetGLView()
            {
                view?.willRemoveSubview(unityView);
                unityView.isHidden = true
            }
        }
    }
}
