package br.com.avancertecnologia.model.drink;

import android.content.Context;

import java.io.Serializable;

import br.com.avancertecnologia.R;
import br.com.avancertecnologia.model.drink.enums.Tipo;

public class Funcionario implements Serializable {

    public static final double CUSTO_INICIAL_FUNCIONARIO_BARRACA_MADEIRA = 100.00;
    public static final double CUSTO_INICIAL_FUNCIONARIO_BARRACA_ELEGANTE = 300.00;
    public static final double CUSTO_INICIAL_FUNCIONARIO_LOJA_PEQUENA = 900.00;
    public static final double CUSTO_INICIAL_FUNCIONARIO_LOJA_MEDIA = 2700.00;
    public static final double CUSTO_INICIAL_FUNCIONARIO_LOJA_GRANDE = 8100.00;
    public static final double CUSTO_INICIAL_FUNCIONARIO_EMPRESA_PEQUENA = 24300.00;
    public static final double CUSTO_INICIAL_FUNCIONARIO_EMPRESA_MEDIA = 72900.00;
    public static final double CUSTO_INICIAL_FUNCIONARIO_EMPRESA_GRANDE = 218700.00;

    public static final int NIVEL_MAXIMO = 40;

    private Nivel FuncionarioBarracaMadeira;
    private Nivel FuncionarioBarracaElegante;
    private Nivel FuncionarioLojaPequena;
    private Nivel FuncionarioLojaMedia;
    private Nivel FuncionarioLojaGrande;
    private Nivel FuncionarioEmpresaPequena;
    private Nivel FuncionarioEmpresaMedia;
    private Nivel FuncionarioEmpresaGrande;

    private transient Context context;

    public Funcionario(Context context) {
        this.context = context;
    }

    //region CreateDefault
    public Funcionario CreateDefault(Context context) {
        this.context = context;
        this.CreateFuncionarioBarracaMadeira();
        this.CreateFuncionarioBarracaElegante();
        this.CreateFuncionarioLojaPequena();
        this.CreateFuncionarioLojaMedia();
        this.CreateFuncionarioLojaGrande();
        this.CreateFuncionarioEmpresaPequena();
        this.CreateFuncionarioEmpresaMedia();
        this.CreateFuncionarioEmpresaGrande();
        return this;
    }

    private void CreateFuncionarioBarracaMadeira() {
        Nivel nivel;
        if (this.getFuncionarioBarracaMadeira() == null) {
            nivel = new Nivel(Tipo.Funcionario_Barraca_Madeira);
        } else {
            nivel = this.getFuncionarioBarracaMadeira();
        }
        nivel.setNome(context.getResources().getString(R.string.funcionario_barraca_madeira));
        nivel.setCusto(CUSTO_INICIAL_FUNCIONARIO_BARRACA_MADEIRA);
        nivel.setCustoInicial(CUSTO_INICIAL_FUNCIONARIO_BARRACA_MADEIRA);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(0);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setFuncionarioBarracaMadeira(nivel);
    }

    private void CreateFuncionarioBarracaElegante() {
        Nivel nivel;
        if (this.getFuncionarioBarracaElegante() == null) {
            nivel = new Nivel(Tipo.Funcionario_Barraca_Elegante);
        } else {
            nivel = this.getFuncionarioBarracaElegante();
        }
        nivel.setNome(context.getResources().getString(R.string.funcionario_barraca_elegante));
        nivel.setCusto(CUSTO_INICIAL_FUNCIONARIO_BARRACA_ELEGANTE);
        nivel.setCustoInicial(CUSTO_INICIAL_FUNCIONARIO_BARRACA_ELEGANTE);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(0);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setFuncionarioBarracaElegante(nivel);
    }

    private void CreateFuncionarioLojaPequena() {
        Nivel nivel;
        if (this.getFuncionarioLojaPequena() == null) {
            nivel = new Nivel(Tipo.Funcionario_Loja_Pequena);
        } else {
            nivel = this.getFuncionarioLojaPequena();
        }
        nivel.setNome(context.getResources().getString(R.string.funcionario_loja_pequena));
        nivel.setCusto(CUSTO_INICIAL_FUNCIONARIO_LOJA_PEQUENA);
        nivel.setCustoInicial(CUSTO_INICIAL_FUNCIONARIO_LOJA_PEQUENA);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(0);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setFuncionarioLojaPequena(nivel);
    }

    private void CreateFuncionarioLojaMedia() {
        Nivel nivel;
        if (this.getFuncionarioLojaMedia() == null) {
            nivel = new Nivel(Tipo.Funcionario_Loja_Media);
        } else {
            nivel = this.getFuncionarioLojaMedia();
        }
        nivel.setNome(context.getResources().getString(R.string.funcionario_loja_media));
        nivel.setCusto(CUSTO_INICIAL_FUNCIONARIO_LOJA_MEDIA);
        nivel.setCustoInicial(CUSTO_INICIAL_FUNCIONARIO_LOJA_MEDIA);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(810000);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setFuncionarioLojaMedia(nivel);
    }

    private void CreateFuncionarioLojaGrande() {
        Nivel nivel;
        if (this.getFuncionarioLojaGrande() == null) {
            nivel = new Nivel(Tipo.Funcionario_Loja_Grande);
        } else {
            nivel = this.getFuncionarioLojaGrande();
        }
        nivel.setNome(context.getResources().getString(R.string.funcionario_loja_grande));
        nivel.setCusto(CUSTO_INICIAL_FUNCIONARIO_LOJA_GRANDE);
        nivel.setCustoInicial(CUSTO_INICIAL_FUNCIONARIO_LOJA_GRANDE);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(0);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setFuncionarioLojaGrande(nivel);
    }

    private void CreateFuncionarioEmpresaPequena() {
        Nivel nivel;
        if (this.getFuncionarioEmpresaPequena() == null) {
            nivel = new Nivel(Tipo.Funcionario_Empresa_Pequena);
        } else {
            nivel = this.getFuncionarioEmpresaPequena();
        }
        nivel.setNome(context.getResources().getString(R.string.funcionario_empresa_pequena));
        nivel.setCusto(CUSTO_INICIAL_FUNCIONARIO_EMPRESA_PEQUENA);
        nivel.setCustoInicial(CUSTO_INICIAL_FUNCIONARIO_EMPRESA_PEQUENA);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(0);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setFuncionarioEmpresaPequena(nivel);
    }

    private void CreateFuncionarioEmpresaMedia() {
        Nivel nivel;
        if (this.getFuncionarioEmpresaMedia() == null) {
            nivel = new Nivel(Tipo.Funcionario_Empresa_Media);
        } else {
            nivel = this.getFuncionarioEmpresaMedia();
        }
        nivel.setNome(context.getResources().getString(R.string.funcionario_empresa_media));
        nivel.setCusto(CUSTO_INICIAL_FUNCIONARIO_EMPRESA_MEDIA);
        nivel.setCustoInicial(CUSTO_INICIAL_FUNCIONARIO_EMPRESA_MEDIA);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(21870000);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setFuncionarioEmpresaMedia(nivel);
    }

    private void CreateFuncionarioEmpresaGrande() {
        Nivel nivel;
        if (this.getFuncionarioEmpresaGrande() == null) {
            nivel = new Nivel(Tipo.Funcionario_Empresa_Grande);
        } else {
            nivel = this.getFuncionarioEmpresaGrande();
        }
        nivel.setNome(context.getResources().getString(R.string.funcionario_empresa_grande));
        nivel.setCusto(CUSTO_INICIAL_FUNCIONARIO_EMPRESA_GRANDE);
        nivel.setCustoInicial(CUSTO_INICIAL_FUNCIONARIO_EMPRESA_GRANDE);
        nivel.setNivel(0);
        nivel.setNivelMaximo(NIVEL_MAXIMO);
        nivel.setTempo(65610000);
        nivel.setValor(0.00);
        nivel.setVendaAutomatica(false);
        this.setFuncionarioEmpresaGrande(nivel);
    }
    //endregion

    //region get set
    public Nivel getFuncionarioBarracaMadeira() {
        return FuncionarioBarracaMadeira;
    }

    public void setFuncionarioBarracaMadeira(Nivel funcionarioBarracaMadeira) {
        FuncionarioBarracaMadeira = funcionarioBarracaMadeira;
    }

    public Nivel getFuncionarioBarracaElegante() {
        return FuncionarioBarracaElegante;
    }

    public void setFuncionarioBarracaElegante(Nivel funcionarioBarracaElegante) {
        FuncionarioBarracaElegante = funcionarioBarracaElegante;
    }

    public Nivel getFuncionarioLojaPequena() {
        return FuncionarioLojaPequena;
    }

    public void setFuncionarioLojaPequena(Nivel funcionarioLojaPequena) {
        FuncionarioLojaPequena = funcionarioLojaPequena;
    }

    public Nivel getFuncionarioLojaMedia() {
        return FuncionarioLojaMedia;
    }

    public void setFuncionarioLojaMedia(Nivel funcionarioLojaMedia) {
        FuncionarioLojaMedia = funcionarioLojaMedia;
    }

    public Nivel getFuncionarioLojaGrande() {
        return FuncionarioLojaGrande;
    }

    public void setFuncionarioLojaGrande(Nivel funcionarioLojaGrande) {
        FuncionarioLojaGrande = funcionarioLojaGrande;
    }

    public Nivel getFuncionarioEmpresaPequena() {
        return FuncionarioEmpresaPequena;
    }

    public void setFuncionarioEmpresaPequena(Nivel funcionarioEmpresaPequena) {
        FuncionarioEmpresaPequena = funcionarioEmpresaPequena;
    }

    public Nivel getFuncionarioEmpresaMedia() {
        return FuncionarioEmpresaMedia;
    }

    public void setFuncionarioEmpresaMedia(Nivel funcionarioEmpresaMedia) {
        FuncionarioEmpresaMedia = funcionarioEmpresaMedia;
    }

    public Nivel getFuncionarioEmpresaGrande() {
        return FuncionarioEmpresaGrande;
    }

    public void setFuncionarioEmpresaGrande(Nivel funcionarioEmpresaGrande) {
        FuncionarioEmpresaGrande = funcionarioEmpresaGrande;
    }
    //endregion
}
