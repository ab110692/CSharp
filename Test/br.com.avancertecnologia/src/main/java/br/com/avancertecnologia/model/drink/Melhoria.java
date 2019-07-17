package br.com.avancertecnologia.model.drink;

import android.content.Context;

import java.io.Serializable;

import br.com.avancertecnologia.R;
import br.com.avancertecnologia.model.drink.enums.Tipo;

public class Melhoria implements Serializable {

    public static final double CUSTO_INICIAL_AGUA = 1.00;
    public static final double CUSTO_INCIAL_GELO = 1.50;
    public static final double CUSTO_INCIAL_Limao = 3.00;
    public static final double CUSTO_INCIAL_Laranja = 6.00;

    public static final int NIVEL_MAXIMO = 100;

    private Nivel Agua;
    private Nivel Gelo;
    private Nivel Limao;
    private Nivel Laranja;

    private transient Context context;

    public Melhoria(Context context) {
        this.context = context;
    }

    //region create default
    public Melhoria CreateDefault(Context context) {
        this.context = context;
        this.CreateAgua();
        this.CreateGelo();
        this.CreateLaranja();
        this.CreateLimao();
        return this;
    }

    private void CreateAgua() {
        if (this.Agua == null) {
            this.Agua = new Nivel(Tipo.Agua);
        }
        this.Agua.setNome(context.getResources().getString(R.string.melhorar_agua));
        this.Agua.setCusto(CUSTO_INICIAL_AGUA);
        this.Agua.setCustoInicial(CUSTO_INICIAL_AGUA);
        this.Agua.setNivel(1);
        this.Agua.setNivelMaximo(NIVEL_MAXIMO);
        this.Agua.setTempo(0);
        this.Agua.setValor(0.01);
        this.Agua.setVendaAutomatica(false);
    }

    private void CreateGelo() {
        if (this.Gelo == null) {
            this.Gelo = new Nivel(Tipo.Gelo);
        }
        this.Gelo.setNome(context.getResources().getString(R.string.adicionar_gelo));
        this.Gelo.setCusto(CUSTO_INCIAL_GELO);
        this.Gelo.setCustoInicial(CUSTO_INCIAL_GELO);
        this.Gelo.setNivel(0);
        this.Gelo.setNivelMaximo(NIVEL_MAXIMO);
        this.Gelo.setTempo(0);
        this.Gelo.setValor(0.00);
        this.Gelo.setVendaAutomatica(false);
    }

    private void CreateLimao() {
        if (this.Limao == null) {
            this.Limao = new Nivel(Tipo.Limao);
        }
        this.Limao.setNome(context.getResources().getString(R.string.adicionar_limao));
        this.Limao.setCusto(CUSTO_INCIAL_Limao);
        this.Limao.setCustoInicial(CUSTO_INCIAL_Limao);
        this.Limao.setNivel(0);
        this.Limao.setNivelMaximo(NIVEL_MAXIMO);
        this.Limao.setTempo(0);
        this.Limao.setValor(0.00);
        this.Limao.setVendaAutomatica(false);
    }

    private void CreateLaranja() {
        if (this.Laranja == null) {
            this.Laranja = new Nivel(Tipo.Laranja);
        }
        this.Laranja.setNome(context.getResources().getString(R.string.adicionar_laranja));
        this.Laranja.setCusto(CUSTO_INCIAL_Laranja);
        this.Laranja.setCustoInicial(CUSTO_INCIAL_Laranja);
        this.Laranja.setNivel(0);
        this.Laranja.setNivelMaximo(NIVEL_MAXIMO);
        this.Laranja.setTempo(0);
        this.Laranja.setValor(0.00);
        this.Laranja.setVendaAutomatica(false);
    }
    //endregion

    //region get set
    public Nivel getAgua() {
        return Agua;
    }

    public void setAgua(Nivel agua) {
        Agua = agua;
    }

    public Nivel getGelo() {
        return Gelo;
    }

    public void setGelo(Nivel gelo) {
        Gelo = gelo;
    }

    public Nivel getLimao() {
        return Limao;
    }

    public void setLimao(Nivel limao) {
        Limao = limao;
    }

    public Nivel getLaranja() {
        return Laranja;
    }

    public void setLaranja(Nivel laranja) {
        Laranja = laranja;
    }
    //endregion

}
