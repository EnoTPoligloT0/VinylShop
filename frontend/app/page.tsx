import React from "react";
import {Categories, Hero, Header, Banner} from "@/components";

export default function Home() {
  return (
    <main className={"overflow-hidden"}>
        <Header/>
      <Hero/>
        <Banner/>
        <Categories/>
    </main>
  );
}
