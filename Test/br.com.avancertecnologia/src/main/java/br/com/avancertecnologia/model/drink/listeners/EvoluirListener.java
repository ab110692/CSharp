package br.com.avancertecnologia.model.drink.listeners;

import br.com.avancertecnologia.model.drink.Bonus;
import br.com.avancertecnologia.model.drink.Nivel;

public interface EvoluirListener {
    void OnEvoluir(Nivel nivel);
    void OnEvoluir(Bonus bonus);
}
