import 'package:flutter/material.dart';
import 'Pages/home_page.dart';
import "Pages/TrendingMovies_page.dart";

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Movie Database',
      theme: ThemeData(
        primarySwatch: Colors.blue,
        useMaterial3: true,
      ),
      debugShowCheckedModeBanner: false,
      home: const HomePage(),
      routes: {
        '/trending': (context) => const TrendingMoviesPage(),
      },
    );
  }
}
