package br.com.avancertecnologia.util;

import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;

import java.util.ArrayList;
import java.util.List;

public class TabPagerAdapter extends FragmentPagerAdapter {

    private List<Fragment> fragments;
    private List<String> titulos;

    public TabPagerAdapter(FragmentManager fragmentManager) {
        super(fragmentManager);

        this.fragments = new ArrayList<>();
        this.titulos = new ArrayList<>();
    }

    @Override
    public Fragment getItem(int i) {
        return this.fragments.get(i);
    }

    @Override
    public int getCount() {
        return this.fragments.size();
    }

    @Nullable
    @Override
    public CharSequence getPageTitle(int position) {
        return this.titulos.get(position);
    }

    public void Add(Fragment fragment, String title) {
        this.fragments.add(fragment);
        this.titulos.add(title);
    }
}
