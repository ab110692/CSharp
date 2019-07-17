package br.com.avancertecnologia.model.drink;

import android.app.Activity;

import java.io.Serializable;
import java.util.Calendar;


public class Personagem implements Serializable {

    private String Nome;

    private double Saldo;

    private int diamante;

    private int Impulso;

    private long TempoImpulso;

    private Melhoria Melhoria;

    private Empresa Empresa;

    private Funcionario Funcionario;

    private Conquista conquista;

    private Estatistica estatistica;

    private Bonus bonus;

    private long lastLogOut;

    public Personagem() {
        this.setImpulso(1);
        Calendar calendar = Calendar.getInstance();
    }

    //region get set
    public String getNome() {
        return Nome;
    }

    public void setNome(String nome) {
        Nome = nome;
    }

    public double getSaldo() {
        return Saldo;
    }

    public void setSaldo(double saldo) {
        Saldo = saldo;
    }

    public int getDiamante() {
        return diamante;
    }

    private void setDiamante(int diamante) {
        this.diamante = diamante;
    }

    public int getImpulso() {
        return Impulso;
    }

    private void setImpulso(int impulso) {
        Impulso = impulso;
    }

    public Melhoria getMelhoria() {
        return Melhoria;
    }

    public void setMelhoria(Melhoria melhoria) {
        Melhoria = melhoria;
    }

    public Empresa getEmpresa() {
        return Empresa;
    }

    public void setEmpresa(Empresa empresa) {
        Empresa = empresa;
    }

    public Funcionario getFuncionario() {
        return Funcionario;
    }

    public void setFuncionario(Funcionario funcionario) {
        Funcionario = funcionario;
    }

    public Conquista getConquista() {
        return conquista;
    }

    public void setConquista(Conquista conquista) {
        this.conquista = conquista;
    }

    public Estatistica getEstatistica() {
        return estatistica;
    }

    public void setEstatistica(Estatistica estatistica) {
        this.estatistica = estatistica;
    }

    public Bonus getBonus() {
        return bonus;
    }

    public void setBonus(Bonus bonus) {
        this.bonus = bonus;
    }

    public long getTempoImpulso() {
        return TempoImpulso;
    }

    private void setTempoImpulso(long tempoImpulso) {
        TempoImpulso = tempoImpulso;
    }

    public long getLastLogOut() {
        return lastLogOut;
    }

    public void setLastLogOut(long lastLogOut) {
        this.lastLogOut = lastLogOut;
    }
    //endregion

    public void impulso() {
        this.setImpulso(2);
        this.setTempoImpulso(300000);
    }

    public void terminoImpulso() {
        this.setImpulso(1);
    }

    public void vender(double valor) {
        double valorBonus = (bonus.getPorcentagem() * valor) / 100;
        valorBonus = valorBonus <= 0 ? 0 : valorBonus;
        setSaldo(getSaldo() + (valor * getImpulso()) + valorBonus);
    }

    public void adicionarDiamante(int quantidade) {
        setDiamante(getDiamante() + quantidade);
    }

    public void removerDiamante(int quantidade) {
        setDiamante(getDiamante() - quantidade);
    }

    public void resetar(Activity activity) {
        this.terminoImpulso();
        this.setSaldo(0.00);
        this.setEstatistica(new Estatistica());
        this.setFuncionario(this.getFuncionario().CreateDefault(activity));
        this.setEmpresa(this.getEmpresa().CreateDefault(activity));
        this.setMelhoria(this.getMelhoria().CreateDefault(activity));
        activity.getIntent().putExtra("Personagem", this);
    }

}
