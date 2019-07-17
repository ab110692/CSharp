package br.com.avancertecnologia.model.drink;

import android.content.Context;

import java.io.Serializable;

import br.com.avancertecnologia.R;
import br.com.avancertecnologia.model.drink.enums.Tipo;

public class Conquista implements Serializable{

    private Nivel MelhoriaAgua10;
    private Nivel MelhoriaAgua25;
    private Nivel MelhoriaAgua50;
    private Nivel MelhoriaAgua100;

    private Nivel MelhoriaGelo10;
    private Nivel MelhoriaGelo25;
    private Nivel MelhoriaGelo50;
    private Nivel MelhoriaGelo100;

    private Nivel MelhoriaLimao10;
    private Nivel MelhoriaLimao25;
    private Nivel MelhoriaLimao50;
    private Nivel MelhoriaLimao100;

    private Nivel MelhoriaLaranja10;
    private Nivel MelhoriaLaranja25;
    private Nivel MelhoriaLaranja50;
    private Nivel MelhoriaLaranja100;

    private Nivel EmppresaMadeira10;
    private Nivel EmppresaMadeira25;
    private Nivel EmppresaMadeira50;
    private Nivel EmppresaMadeira100;

    private Nivel EmppresaElegante10;
    private Nivel EmppresaElegante25;
    private Nivel EmppresaElegante50;
    private Nivel EmppresaElegante100;

    private Nivel EmppresaLojaPequena10;
    private Nivel EmppresaLojaPequena25;
    private Nivel EmppresaLojaPequena50;
    private Nivel EmppresaLojaPequena100;

    private Nivel EmppresaLojaMedia10;
    private Nivel EmppresaLojaMedia25;
    private Nivel EmppresaLojaMedia50;
    private Nivel EmppresaLojaMedia100;

    private Nivel EmppresaLojaGrande10;
    private Nivel EmppresaLojaGrande25;
    private Nivel EmppresaLojaGrande50;
    private Nivel EmppresaLojaGrande100;

    private Nivel EmppresaEmpresaPequena10;
    private Nivel EmppresaEmpresaPequena25;
    private Nivel EmppresaEmpresaPequena50;
    private Nivel EmppresaEmpresaPequena100;

    private Nivel EmppresaEmpresaMedia10;
    private Nivel EmppresaEmpresaMedia25;
    private Nivel EmppresaEmpresaMedia50;
    private Nivel EmppresaEmpresaMedia100;

    private Nivel EmppresaEmpresaGrande10;
    private Nivel EmppresaEmpresaGrande25;
    private Nivel EmppresaEmpresaGrande50;
    private Nivel EmppresaEmpresaGrande100;

    private transient Context context;

    public Conquista(Context context) {
        this.context = context;
    }

    public Conquista CreateDefault(Context context){
            this.context = context;
        this.createMelhoriaAgua10();
        this.createMelhoriaAgua25();
        this.createMelhoriaAgua50();
        this.createMelhoriaAgua100();
        this.createMelhoriaGelo10();
        this.createMelhoriaGelo25();
        this.createMelhoriaGelo50();
        this.createMelhoriaGelo100();
        this.createMelhoriaLimao10();
        this.createMelhoriaLimao25();
        this.createMelhoriaLimao50();
        this.createMelhoriaLimao100();
        this.createMelhoriaLaranja10();
        this.createMelhoriaLaranja25();
        this.createMelhoriaLaranja50();
        this.createMelhoriaLaranja100();
        return this;
    }

    public void createMelhoriaAgua10(){
        Nivel nivel;
        if(getMelhoriaAgua10() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Agua);
        }
        else{
            nivel = getMelhoriaAgua10();
        }
        nivel.setNome(this.context.getResources().getString(R.string.agua_da_fonte));
        nivel.setNivel(0);
        nivel.setNivelMaximo(10);
        nivel.setTempo(1);
        nivel.setAtivo(true);
        setMelhoriaAgua10(nivel);
    }

    public void createMelhoriaAgua25(){
        Nivel nivel;
        if(getMelhoriaAgua25() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Agua);
        }
        else{
            nivel = getMelhoriaAgua25();
        }
        nivel.setNome(this.context.getResources().getString(R.string.agua_maravilhosa));
        nivel.setNivel(0);
        nivel.setNivelMaximo(25);
        nivel.setTempo(2);
        nivel.setAtivo(true);
        setMelhoriaAgua25(nivel);
    }

    public void createMelhoriaAgua50(){
        Nivel nivel;
        if(getMelhoriaAgua50() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Agua);
        }
        else{
            nivel = getMelhoriaAgua50();
        }
        nivel.setNome(this.context.getResources().getString(R.string.melhor_agua_cidade));
        nivel.setNivel(0);
        nivel.setNivelMaximo(50);
        nivel.setTempo(5);
        nivel.setAtivo(true);
        setMelhoriaAgua50(nivel);
    }

    public void createMelhoriaAgua100(){
        Nivel nivel;
        if(getMelhoriaAgua100() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Agua);
        }
        else{
            nivel = getMelhoriaAgua100();
        }
        nivel.setNome(this.context.getResources().getString(R.string.melhor_agua_mundo));
        nivel.setNivel(0);
        nivel.setNivelMaximo(100);
        nivel.setTempo(10);
        nivel.setAtivo(true);
        setMelhoriaAgua100(nivel);
    }

    public void createMelhoriaGelo10(){
        Nivel nivel;
        if(getMelhoriaGelo10() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Gelo);
        }
        else{
            nivel = getMelhoriaGelo10();
        }
        nivel.setNome(this.context.getResources().getString(R.string.gelo_gelado));
        nivel.setNivel(0);
        nivel.setNivelMaximo(10);
        nivel.setTempo(1);
        nivel.setAtivo(true);
        setMelhoriaGelo10(nivel);
    }

    public void createMelhoriaGelo25(){
        Nivel nivel;
        if(getMelhoriaGelo25() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Gelo);
        }
        else{
            nivel = getMelhoriaGelo25();
        }
        nivel.setNome(this.context.getResources().getString(R.string.cubo_perfeito));
        nivel.setNivel(0);
        nivel.setNivelMaximo(25);
        nivel.setTempo(2);
        nivel.setAtivo(true);
        setMelhoriaGelo25(nivel);
    }

    public void createMelhoriaGelo50(){
        Nivel nivel;
        if(getMelhoriaGelo50() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Gelo);
        }
        else{
            nivel = getMelhoriaGelo50();
        }
        nivel.setNome(this.context.getResources().getString(R.string.gelo_impressionante));
        nivel.setNivel(0);
        nivel.setNivelMaximo(50);
        nivel.setTempo(5);
        nivel.setAtivo(true);
        setMelhoriaGelo50(nivel);
    }

    public void createMelhoriaGelo100(){
        Nivel nivel;
        if(getMelhoriaGelo100() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Gelo);
        }
        else{
            nivel = getMelhoriaGelo100();
        }
        nivel.setNome(this.context.getResources().getString(R.string.o_melhor_gelo_do_mundo));
        nivel.setNivel(0);
        nivel.setNivelMaximo(100);
        nivel.setTempo(10);
        nivel.setAtivo(true);
        setMelhoriaGelo100(nivel);
    }

    public void createMelhoriaLimao10(){
        Nivel nivel;
        if(getMelhoriaLimao10() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Limao);
        }
        else{
            nivel = getMelhoriaLimao10();
        }
        nivel.setNome(this.context.getResources().getString(R.string.limao_fresco));
        nivel.setNivel(0);
        nivel.setNivelMaximo(10);
        nivel.setTempo(1);
        nivel.setAtivo(true);
        setMelhoriaLimao10(nivel);
    }

    public void createMelhoriaLimao25(){
        Nivel nivel;
        if(getMelhoriaLimao25() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Limao);
        }
        else{
            nivel = getMelhoriaLimao25();
        }
        nivel.setNome(this.context.getResources().getString(R.string.limao_delicioso));
        nivel.setNivel(0);
        nivel.setNivelMaximo(25);
        nivel.setTempo(2);
        nivel.setAtivo(true);
        setMelhoriaLimao25(nivel);
    }

    public void createMelhoriaLimao50(){
        Nivel nivel;
        if(getMelhoriaLimao50() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Limao);
        }
        else{
            nivel = getMelhoriaLimao50();
        }
        nivel.setNome(this.context.getResources().getString(R.string.limao_irresistivel));
        nivel.setNivel(0);
        nivel.setNivelMaximo(50);
        nivel.setTempo(5);
        nivel.setAtivo(true);
        setMelhoriaLimao50(nivel);
    }

    public void createMelhoriaLimao100(){
        Nivel nivel;
        if(getMelhoriaLimao100() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Limao);
        }
        else{
            nivel = getMelhoriaLimao100();
        }
        nivel.setNome(this.context.getResources().getString(R.string.o_melhor_limao_do_mundo));
        nivel.setNivel(0);
        nivel.setNivelMaximo(100);
        nivel.setTempo(10);
        nivel.setAtivo(true);
        setMelhoriaLimao100(nivel);
    }

    public void createMelhoriaLaranja10(){
        Nivel nivel;
        if(getMelhoriaLaranja10() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Laranja);
        }
        else{
            nivel = getMelhoriaLaranja10();
        }
        nivel.setNome(this.context.getResources().getString(R.string.laranja_fresca));
        nivel.setNivel(0);
        nivel.setNivelMaximo(10);
        nivel.setTempo(1);
        nivel.setAtivo(true);
        setMelhoriaLaranja10(nivel);
    }

    public void createMelhoriaLaranja25(){
        Nivel nivel;
        if(getMelhoriaLaranja25() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Laranja);
        }
        else{
            nivel = getMelhoriaLaranja25();
        }
        nivel.setNome(this.context.getResources().getString(R.string.laranja_deliciosa));
        nivel.setNivel(0);
        nivel.setNivelMaximo(25);
        nivel.setTempo(2);
        nivel.setAtivo(true);
        setMelhoriaLaranja25(nivel);
    }

    public void createMelhoriaLaranja50(){
        Nivel nivel;
        if(getMelhoriaLaranja50() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Laranja);
        }
        else{
            nivel = getMelhoriaLaranja50();
        }
        nivel.setNome(this.context.getResources().getString(R.string.laranja_irresistivel));
        nivel.setNivel(0);
        nivel.setNivelMaximo(50);
        nivel.setTempo(5);
        nivel.setAtivo(true);
        setMelhoriaLaranja50(nivel);
    }

    public void createMelhoriaLaranja100(){
        Nivel nivel;
        if(getMelhoriaLaranja100() == null) {
            nivel = new Nivel(Tipo.Conquista_Melhoria_Laranja);
        }
        else{
            nivel = getMelhoriaLaranja100();
        }
        nivel.setNome(this.context.getResources().getString(R.string.a_melhor_laranja_do_mundo));
        nivel.setNivel(0);
        nivel.setNivelMaximo(100);
        nivel.setTempo(10);
        nivel.setAtivo(true);
        setMelhoriaLaranja100(nivel);
    }

    //region Get Set
    public Nivel getMelhoriaAgua10() {
        return MelhoriaAgua10;
    }

    public void setMelhoriaAgua10(Nivel melhoriaAgua10) {
        MelhoriaAgua10 = melhoriaAgua10;
    }

    public Nivel getMelhoriaAgua25() {
        return MelhoriaAgua25;
    }

    public void setMelhoriaAgua25(Nivel melhoriaAgua25) {
        MelhoriaAgua25 = melhoriaAgua25;
    }

    public Nivel getMelhoriaAgua50() {
        return MelhoriaAgua50;
    }

    public void setMelhoriaAgua50(Nivel melhoriaAgua50) {
        MelhoriaAgua50 = melhoriaAgua50;
    }

    public Nivel getMelhoriaAgua100() {
        return MelhoriaAgua100;
    }

    public void setMelhoriaAgua100(Nivel melhoriaAgua100) {
        MelhoriaAgua100 = melhoriaAgua100;
    }

    public Nivel getMelhoriaGelo10() {
        return MelhoriaGelo10;
    }

    public void setMelhoriaGelo10(Nivel melhoriaGelo10) {
        MelhoriaGelo10 = melhoriaGelo10;
    }

    public Nivel getMelhoriaGelo25() {
        return MelhoriaGelo25;
    }

    public void setMelhoriaGelo25(Nivel melhoriaGelo25) {
        MelhoriaGelo25 = melhoriaGelo25;
    }

    public Nivel getMelhoriaGelo50() {
        return MelhoriaGelo50;
    }

    public void setMelhoriaGelo50(Nivel melhoriaGelo50) {
        MelhoriaGelo50 = melhoriaGelo50;
    }

    public Nivel getMelhoriaGelo100() {
        return MelhoriaGelo100;
    }

    public void setMelhoriaGelo100(Nivel melhoriaGelo100) {
        MelhoriaGelo100 = melhoriaGelo100;
    }

    public Nivel getMelhoriaLimao10() {
        return MelhoriaLimao10;
    }

    public void setMelhoriaLimao10(Nivel melhoriaLimao10) {
        MelhoriaLimao10 = melhoriaLimao10;
    }

    public Nivel getMelhoriaLimao25() {
        return MelhoriaLimao25;
    }

    public void setMelhoriaLimao25(Nivel melhoriaLimao25) {
        MelhoriaLimao25 = melhoriaLimao25;
    }

    public Nivel getMelhoriaLimao50() {
        return MelhoriaLimao50;
    }

    public void setMelhoriaLimao50(Nivel melhoriaLimao50) {
        MelhoriaLimao50 = melhoriaLimao50;
    }

    public Nivel getMelhoriaLimao100() {
        return MelhoriaLimao100;
    }

    public void setMelhoriaLimao100(Nivel melhoriaLimao100) {
        MelhoriaLimao100 = melhoriaLimao100;
    }

    public Nivel getMelhoriaLaranja10() {
        return MelhoriaLaranja10;
    }

    public void setMelhoriaLaranja10(Nivel melhoriaLaranja10) {
        MelhoriaLaranja10 = melhoriaLaranja10;
    }

    public Nivel getMelhoriaLaranja25() {
        return MelhoriaLaranja25;
    }

    public void setMelhoriaLaranja25(Nivel melhoriaLaranja25) {
        MelhoriaLaranja25 = melhoriaLaranja25;
    }

    public Nivel getMelhoriaLaranja50() {
        return MelhoriaLaranja50;
    }

    public void setMelhoriaLaranja50(Nivel melhoriaLaranja50) {
        MelhoriaLaranja50 = melhoriaLaranja50;
    }

    public Nivel getMelhoriaLaranja100() {
        return MelhoriaLaranja100;
    }

    public void setMelhoriaLaranja100(Nivel melhoriaLaranja100) {
        MelhoriaLaranja100 = melhoriaLaranja100;
    }

    public Nivel getEmppresaMadeira10() {
        return EmppresaMadeira10;
    }

    public void setEmppresaMadeira10(Nivel emppresaMadeira10) {
        EmppresaMadeira10 = emppresaMadeira10;
    }

    public Nivel getEmppresaMadeira25() {
        return EmppresaMadeira25;
    }

    public void setEmppresaMadeira25(Nivel emppresaMadeira25) {
        EmppresaMadeira25 = emppresaMadeira25;
    }

    public Nivel getEmppresaMadeira50() {
        return EmppresaMadeira50;
    }

    public void setEmppresaMadeira50(Nivel emppresaMadeira50) {
        EmppresaMadeira50 = emppresaMadeira50;
    }

    public Nivel getEmppresaMadeira100() {
        return EmppresaMadeira100;
    }

    public void setEmppresaMadeira100(Nivel emppresaMadeira100) {
        EmppresaMadeira100 = emppresaMadeira100;
    }

    public Nivel getEmppresaElegante10() {
        return EmppresaElegante10;
    }

    public void setEmppresaElegante10(Nivel emppresaElegante10) {
        EmppresaElegante10 = emppresaElegante10;
    }

    public Nivel getEmppresaElegante25() {
        return EmppresaElegante25;
    }

    public void setEmppresaElegante25(Nivel emppresaElegante25) {
        EmppresaElegante25 = emppresaElegante25;
    }

    public Nivel getEmppresaElegante50() {
        return EmppresaElegante50;
    }

    public void setEmppresaElegante50(Nivel emppresaElegante50) {
        EmppresaElegante50 = emppresaElegante50;
    }

    public Nivel getEmppresaElegante100() {
        return EmppresaElegante100;
    }

    public void setEmppresaElegante100(Nivel emppresaElegante100) {
        EmppresaElegante100 = emppresaElegante100;
    }

    public Nivel getEmppresaLojaPequena10() {
        return EmppresaLojaPequena10;
    }

    public void setEmppresaLojaPequena10(Nivel emppresaLojaPequena10) {
        EmppresaLojaPequena10 = emppresaLojaPequena10;
    }

    public Nivel getEmppresaLojaPequena25() {
        return EmppresaLojaPequena25;
    }

    public void setEmppresaLojaPequena25(Nivel emppresaLojaPequena25) {
        EmppresaLojaPequena25 = emppresaLojaPequena25;
    }

    public Nivel getEmppresaLojaPequena50() {
        return EmppresaLojaPequena50;
    }

    public void setEmppresaLojaPequena50(Nivel emppresaLojaPequena50) {
        EmppresaLojaPequena50 = emppresaLojaPequena50;
    }

    public Nivel getEmppresaLojaPequena100() {
        return EmppresaLojaPequena100;
    }

    public void setEmppresaLojaPequena100(Nivel emppresaLojaPequena100) {
        EmppresaLojaPequena100 = emppresaLojaPequena100;
    }

    public Nivel getEmppresaLojaMedia10() {
        return EmppresaLojaMedia10;
    }

    public void setEmppresaLojaMedia10(Nivel emppresaLojaMedia10) {
        EmppresaLojaMedia10 = emppresaLojaMedia10;
    }

    public Nivel getEmppresaLojaMedia25() {
        return EmppresaLojaMedia25;
    }

    public void setEmppresaLojaMedia25(Nivel emppresaLojaMedia25) {
        EmppresaLojaMedia25 = emppresaLojaMedia25;
    }

    public Nivel getEmppresaLojaMedia50() {
        return EmppresaLojaMedia50;
    }

    public void setEmppresaLojaMedia50(Nivel emppresaLojaMedia50) {
        EmppresaLojaMedia50 = emppresaLojaMedia50;
    }

    public Nivel getEmppresaLojaMedia100() {
        return EmppresaLojaMedia100;
    }

    public void setEmppresaLojaMedia100(Nivel emppresaLojaMedia100) {
        EmppresaLojaMedia100 = emppresaLojaMedia100;
    }

    public Nivel getEmppresaLojaGrande10() {
        return EmppresaLojaGrande10;
    }

    public void setEmppresaLojaGrande10(Nivel emppresaLojaGrande10) {
        EmppresaLojaGrande10 = emppresaLojaGrande10;
    }

    public Nivel getEmppresaLojaGrande25() {
        return EmppresaLojaGrande25;
    }

    public void setEmppresaLojaGrande25(Nivel emppresaLojaGrande25) {
        EmppresaLojaGrande25 = emppresaLojaGrande25;
    }

    public Nivel getEmppresaLojaGrande50() {
        return EmppresaLojaGrande50;
    }

    public void setEmppresaLojaGrande50(Nivel emppresaLojaGrande50) {
        EmppresaLojaGrande50 = emppresaLojaGrande50;
    }

    public Nivel getEmppresaLojaGrande100() {
        return EmppresaLojaGrande100;
    }

    public void setEmppresaLojaGrande100(Nivel emppresaLojaGrande100) {
        EmppresaLojaGrande100 = emppresaLojaGrande100;
    }

    public Nivel getEmppresaEmpresaPequena10() {
        return EmppresaEmpresaPequena10;
    }

    public void setEmppresaEmpresaPequena10(Nivel emppresaEmpresaPequena10) {
        EmppresaEmpresaPequena10 = emppresaEmpresaPequena10;
    }

    public Nivel getEmppresaEmpresaPequena25() {
        return EmppresaEmpresaPequena25;
    }

    public void setEmppresaEmpresaPequena25(Nivel emppresaEmpresaPequena25) {
        EmppresaEmpresaPequena25 = emppresaEmpresaPequena25;
    }

    public Nivel getEmppresaEmpresaPequena50() {
        return EmppresaEmpresaPequena50;
    }

    public void setEmppresaEmpresaPequena50(Nivel emppresaEmpresaPequena50) {
        EmppresaEmpresaPequena50 = emppresaEmpresaPequena50;
    }

    public Nivel getEmppresaEmpresaPequena100() {
        return EmppresaEmpresaPequena100;
    }

    public void setEmppresaEmpresaPequena100(Nivel emppresaEmpresaPequena100) {
        EmppresaEmpresaPequena100 = emppresaEmpresaPequena100;
    }

    public Nivel getEmppresaEmpresaMedia10() {
        return EmppresaEmpresaMedia10;
    }

    public void setEmppresaEmpresaMedia10(Nivel emppresaEmpresaMedia10) {
        EmppresaEmpresaMedia10 = emppresaEmpresaMedia10;
    }

    public Nivel getEmppresaEmpresaMedia25() {
        return EmppresaEmpresaMedia25;
    }

    public void setEmppresaEmpresaMedia25(Nivel emppresaEmpresaMedia25) {
        EmppresaEmpresaMedia25 = emppresaEmpresaMedia25;
    }

    public Nivel getEmppresaEmpresaMedia50() {
        return EmppresaEmpresaMedia50;
    }

    public void setEmppresaEmpresaMedia50(Nivel emppresaEmpresaMedia50) {
        EmppresaEmpresaMedia50 = emppresaEmpresaMedia50;
    }

    public Nivel getEmppresaEmpresaMedia100() {
        return EmppresaEmpresaMedia100;
    }

    public void setEmppresaEmpresaMedia100(Nivel emppresaEmpresaMedia100) {
        EmppresaEmpresaMedia100 = emppresaEmpresaMedia100;
    }

    public Nivel getEmppresaEmpresaGrande10() {
        return EmppresaEmpresaGrande10;
    }

    public void setEmppresaEmpresaGrande10(Nivel emppresaEmpresaGrande10) {
        EmppresaEmpresaGrande10 = emppresaEmpresaGrande10;
    }

    public Nivel getEmppresaEmpresaGrande25() {
        return EmppresaEmpresaGrande25;
    }

    public void setEmppresaEmpresaGrande25(Nivel emppresaEmpresaGrande25) {
        EmppresaEmpresaGrande25 = emppresaEmpresaGrande25;
    }

    public Nivel getEmppresaEmpresaGrande50() {
        return EmppresaEmpresaGrande50;
    }

    public void setEmppresaEmpresaGrande50(Nivel emppresaEmpresaGrande50) {
        EmppresaEmpresaGrande50 = emppresaEmpresaGrande50;
    }

    public Nivel getEmppresaEmpresaGrande100() {
        return EmppresaEmpresaGrande100;
    }

    public void setEmppresaEmpresaGrande100(Nivel emppresaEmpresaGrande100) {
        EmppresaEmpresaGrande100 = emppresaEmpresaGrande100;
    }
    //endregion
}
