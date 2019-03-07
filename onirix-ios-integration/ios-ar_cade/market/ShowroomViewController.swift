//
//  ShowroomViewController.swift
//  market
//
//  Created by Victor Fernández on 27/02/2019.
//  Copyright © 2019 Victor Fernández. All rights reserved.
//

import UIKit

class ShowroomViewController: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
    }
    

    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destination.
        // Pass the selected object to the new view controller.
    }
    */

    @IBAction func goIntoAction(_ sender: Any) {
        performSegue(withIdentifier: "goToOnirix", sender: self)
    }
}
