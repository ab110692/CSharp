package br.com.avancertecnologia.model.drink;

import android.content.Context;

import java.io.Serializable;

import br.com.avancertecnologia.R;
import br.com.avancertecnologia.model.drink.enums.Tipo;

public class Empresa implements Serializable {

    public static final double CUSTO_INICIAL_BARRACA_MADEIRA = 500.00;
    public static final double CUSTO_INICIAL_BARRACA_Elegante = 1500.00;
    public static final double CUSTO_INICIAL_LOJA_PEQUENA = 13500.00;
    public static final double CUSTO_INICIAL_LOJA_MEDIA = 35000.00;
    public static final double CUSTO_INICIAL_LOJA_GRANDE = 40500.00;
    public static final double CUSTO_INICIAL_EMPRESA_PEQUENA = 121500.00;
    public static final double CUSTO_INICIAL_EMPRESA_MEDIA = 364500.00;
    public static final double CUSTO_INICIAL_EMPRESA_GRANDE = 1093500.00;

    public static final long TEMPO_INICIAL_BARRACA_MADEIRA = 30000;
    public static final long TEMPO_INICIAL_BARRACA_ELEGANTE = 90000;
    public static final long TEMPO_INICIAL_LOJA_PEQUENA = 270000;
    public static final long TEMPO_INICIAL_LOJA_MEDIA = 810000;
    public static final long TEMPO_INICIAL_LOJA_GRANDE = 2430000;
    public static final long TEMPO_INICIAL_EMPRESA_PEQUENA = 7290000;
    public static final long TEMPO_INICIAL_EMPRESA_MEDIA = 21870000;
    public static final long TEMPO_INICIAL_EMPRESA_GRANDE = 65610000;

    public static final int NIVEL_MAXIMO = 100;

    private Nivel BarracaMadeira;
    private Nivel BarracaElegante;
    private Nivel LojaPequena;
    private Nivel LojaMedia;
    private Nivel LojaGrande;
    private Nivel EmpresaPequena;
    private Nivel EmpresaMedia;
    private Nivel EmpresaGrande;

    private transient Context context;

    public Empresa(Context context) {
        this.context = context;
    }

    //region CreateDefault
    public Empresa CreateDefault(Context context) {
        this.context = context;
        this.CreateBarracaMadeira();
        this.CreateBarracaElegante();
        this.CreateLojaPequena();
        this.CreateLojaMedia();
        this.CreateLojaGrande();
        this.CreateEmpresaPequena();
        this.CreateEmpresaMedia();
        this.CreateEmpresaGrande();
        return this;
    }

    private void CreateBarracaMadeira() {
        Nivel nivel;
        if (this.getBarracaMadeira() == null) {
            nivel = new Nivel(Tipo.Barraco_Madeira);
        } else {
            nivel = this.getBarracaMadeira();
        }
        nivel.setNome(context.getResources().getString(R.string.barraca_madeira));
        nivel.setCusto(CUSTO_INICIAL_BARRACA_MADEIRA);
        nivel.setCustoInicial(CUSTO_INICIAL_BARRACA_MADEIRA);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(TEMPO_INICIAL_BARRACA_MADEIRA);
        nivel.setDefaultTempo(TEMPO_INICIAL_BARRACA_MADEIRA);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setBarracaMadeira(nivel);
    }

    private void CreateBarracaElegante() {
        Nivel nivel;
        if (this.getBarracaElegante() == null) {
            nivel = new Nivel(Tipo.Barraco_Elegante);
        } else {
            nivel = this.getBarracaElegante();
        }
        nivel.setNome(context.getResources().getString(R.string.barraca_elegante));
        nivel.setCusto(CUSTO_INICIAL_BARRACA_Elegante);
        nivel.setCustoInicial(CUSTO_INICIAL_BARRACA_Elegante);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(TEMPO_INICIAL_BARRACA_ELEGANTE);
        nivel.setDefaultTempo(TEMPO_INICIAL_BARRACA_ELEGANTE);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setBarracaElegante(nivel);
    }

    private void CreateLojaPequena() {
        Nivel nivel;
        if (this.getLojaPequena() == null) {
            nivel = new Nivel(Tipo.Loja_Pequena);
        } else {
            nivel = this.getLojaPequena();
        }
        nivel.setNome(context.getResources().getString(R.string.loja_pequena));
        nivel.setCusto(CUSTO_INICIAL_LOJA_PEQUENA);
        nivel.setCustoInicial(CUSTO_INICIAL_LOJA_PEQUENA);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(TEMPO_INICIAL_LOJA_PEQUENA);
        nivel.setDefaultTempo(TEMPO_INICIAL_LOJA_PEQUENA);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setLojaPequena(nivel);
    }

    private void CreateLojaMedia() {
        Nivel nivel;
        if (this.getLojaMedia() == null) {
            nivel = new Nivel(Tipo.Loja_Media);
        } else {
            nivel = this.getLojaMedia();
        }
        nivel.setNome(context.getResources().getString(R.string.loja_media));
        nivel.setCusto(CUSTO_INICIAL_LOJA_MEDIA);
        nivel.setCustoInicial(CUSTO_INICIAL_LOJA_MEDIA);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(TEMPO_INICIAL_LOJA_MEDIA);
        nivel.setDefaultTempo(TEMPO_INICIAL_LOJA_MEDIA);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setLojaMedia(nivel);
    }

    private void CreateLojaGrande() {
        Nivel nivel;
        if (this.getLojaGrande() == null) {
            nivel = new Nivel(Tipo.Loja_Grande);
        } else {
            nivel = this.getLojaGrande();
        }
        nivel.setNome(context.getResources().getString(R.string.loja_grande));
        nivel.setCusto(CUSTO_INICIAL_LOJA_GRANDE);
        nivel.setCustoInicial(CUSTO_INICIAL_LOJA_GRANDE);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(TEMPO_INICIAL_LOJA_GRANDE);
        nivel.setDefaultTempo(TEMPO_INICIAL_LOJA_GRANDE);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setLojaGrande(nivel);
    }

    private void CreateEmpresaPequena() {
        Nivel nivel;
        if (this.getEmpresaPequena() == null) {
            nivel = new Nivel(Tipo.Empresa_Pequena);
        } else {
            nivel = this.getEmpresaPequena();
        }
        nivel.setNome(context.getResources().getString(R.string.empresa_pequena));
        nivel.setCusto(CUSTO_INICIAL_EMPRESA_PEQUENA);
        nivel.setCustoInicial(CUSTO_INICIAL_EMPRESA_PEQUENA);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(TEMPO_INICIAL_EMPRESA_PEQUENA);
        nivel.setDefaultTempo(TEMPO_INICIAL_EMPRESA_PEQUENA);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setEmpresaPequena(nivel);
    }

    private void CreateEmpresaMedia() {
        Nivel nivel;
        if (this.getEmpresaMedia() == null) {
            nivel = new Nivel(Tipo.Empresa_Media);
        } else {
            nivel = this.getEmpresaMedia();
        }
        nivel.setNome(context.getResources().getString(R.string.empresa_media));
        nivel.setCusto(CUSTO_INICIAL_EMPRESA_MEDIA);
        nivel.setCustoInicial(CUSTO_INICIAL_EMPRESA_MEDIA);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(TEMPO_INICIAL_EMPRESA_MEDIA);
        nivel.setDefaultTempo(TEMPO_INICIAL_EMPRESA_MEDIA);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setEmpresaMedia(nivel);
    }

    private void CreateEmpresaGrande() {
        Nivel nivel;
        if (this.getEmpresaGrande() == null) {
            nivel = new Nivel(Tipo.Empresa_Grande);
        } else {
            nivel = this.getEmpresaGrande();
        }
        nivel.setNome(context.getResources().getString(R.string.empresa_grande));
        nivel.setCusto(CUSTO_INICIAL_EMPRESA_GRANDE);
        nivel.setCustoInicial(CUSTO_INICIAL_EMPRESA_GRANDE);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(TEMPO_INICIAL_EMPRESA_GRANDE);
        nivel.setDefaultTempo(TEMPO_INICIAL_EMPRESA_GRANDE);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setEmpresaGrande(nivel);
    }
    //endregion

    //region get set
    public Nivel getBarracaMadeira() {
        return BarracaMadeira;
    }

    public void setBarracaMadeira(Nivel barracaMadeira) {
        BarracaMadeira = barracaMadeira;
    }

    public Nivel getBarracaElegante() {
        return BarracaElegante;
    }

    public void setBarracaElegante(Nivel barracaElegante) {
        BarracaElegante = barracaElegante;
    }

    public Nivel getLojaPequena() {
        return LojaPequena;
    }

    public void setLojaPequena(Nivel lojaPequena) {
        LojaPequena = lojaPequena;
    }

    public Nivel getLojaMedia() {
        return LojaMedia;
    }

    public void setLojaMedia(Nivel lojaMedia) {
        LojaMedia = lojaMedia;
    }

    public Nivel getLojaGrande() {
        return LojaGrande;
    }

    public void setLojaGrande(Nivel lojaGrande) {
        LojaGrande = lojaGrande;
    }

    public Nivel getEmpresaPequena() {
        return EmpresaPequena;
    }

    public void setEmpresaPequena(Nivel empresaPequena) {
        EmpresaPequena = empresaPequena;
    }

    public Nivel getEmpresaMedia() {
        return EmpresaMedia;
    }

    public void setEmpresaMedia(Nivel empresaMedia) {
        EmpresaMedia = empresaMedia;
    }

    public Nivel getEmpresaGrande() {
        return EmpresaGrande;
    }

    public void setEmpresaGrande(Nivel empresaGrande) {
        EmpresaGrande = empresaGrande;
    }
    //endregion
}
