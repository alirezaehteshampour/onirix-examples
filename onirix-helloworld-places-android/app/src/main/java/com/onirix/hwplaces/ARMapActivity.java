package com.onirix.hwplaces;

import android.content.Context;
import android.location.Location;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.WindowManager;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.onirix.places.config.MapConfig;
import com.onirix.places.config.PlaceConfig;
import com.onirix.places.config.routes.DestinationIndicationComponent;
import com.onirix.places.config.routes.DirectionArrowComponent;
import com.onirix.places.config.routes.RouteConfig;
import com.onirix.places.config.routes.RouteHUDComponent;
import com.onirix.places.model.PlaceARWrapper;
import com.onirix.places.ui.MapActivity;
import com.onirix.places_core.model.Map;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import butterknife.BindView;
import butterknife.ButterKnife;

public class ARMapActivity extends MapActivity {

    private static final String TOKEN = "<YOUR_PROJECT_TOKEN_HERE>";
    private static final String MAP_OID = "<YOUR_MAP_OID_HERE>";

    @BindView(R.id.loading_view)
    View loadingView;

    @BindView(R.id.loading_text)
    TextView loadingText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        RelativeLayout rootLayout = findViewById(com.onirix.places.R.id.activity_main);
        LayoutInflater inflater = (LayoutInflater) getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        if (inflater != null) {
            inflater.inflate(R.layout.activity_ar_map, rootLayout);
            inflater.inflate(R.layout.ar_map_loading, rootLayout);
        }

        ButterKnife.bind(this);

        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);

        MapConfig mapConfig = new MapConfig(this)
                .withVisionDistance(100)
                .withShowRadar(true)
                .withRadarDistance(100)
                .withUseUpdateRadius(true)
                .withUpdateRatio(0.5f)
                .withUpdateRadius(2 * 100);

        PlaceConfig placeConfig = new PlaceConfig(this)
                .withShowDistance(true)
                .withShowMarker(true)
                .withShowName(true);

        mapConfig.setConfigForCategory("default", placeConfig);
        loadOnirixMap(MAP_OID, TOKEN, mapConfig);
    }

    @Override
    public void onMapLoaded(Map map, String error) {

        if (map != null) {
            loadingView.setVisibility(View.GONE);
        }

    }

    @Override
    public void onLocationUpdate(Location location) {

    }

    @Override
    public void onPlaceTouched(@NonNull PlaceARWrapper place) {

    }

    @Override
    public void onFocusChanged(@Nullable PlaceARWrapper newFocus) {

    }
}
