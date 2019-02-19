package com.onirix.hwplaces;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;

import java.util.ArrayList;
import java.util.List;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;

public class MainActivity extends AppCompatActivity {

    private List<String> permissionsToRequest;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        boolean hasPermissions = arePermissionsGranted(Manifest.permission.CAMERA,
                Manifest.permission.ACCESS_COARSE_LOCATION, Manifest.permission.ACCESS_FINE_LOCATION);

        if (hasPermissions) {
            startActivity(new Intent(this, ARMapActivity.class));
        } else {
            requestPermissions();
        }
    }

    public boolean arePermissionsGranted(String... permissions) {

        permissionsToRequest = new ArrayList<>();

        for (String permission : permissions) {
            if (!(ActivityCompat.checkSelfPermission(this, permission) == PackageManager.PERMISSION_GRANTED)) {
                permissionsToRequest.add(permission);
            }
        }

        return permissionsToRequest.size() == 0;
    }

    public void requestPermissions() {
        ActivityCompat.requestPermissions(this, permissionsToRequest.toArray(new String[] {}), 1);
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String permissions[], @NonNull int[] grantResults) {
        if (grantResults.length > 0) {
            int deniedPermissions = 0;
            for (int grantResult : grantResults) {
                if (grantResult == PackageManager.PERMISSION_DENIED) {
                    deniedPermissions++;
                }
            }

            if (deniedPermissions > 0) {
                requestPermissions();
            } else {
                startActivity(new Intent(this, ARMapActivity.class));
            }
        } else {
            requestPermissions();
        }
    }
}
