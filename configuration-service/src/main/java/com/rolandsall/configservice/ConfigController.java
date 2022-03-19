package com.rolandsall.configservice;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class ConfigController {

    @Value("${encrypt.key}")
    private String Key;

    @GetMapping("/api/v1/key")
    public String Configs(){
        return "Key: " + Key;
    }
}
